using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TakeHomeAssignmentAPI.Data;
using TakeHomeAssignmentAPI.Repositories;
using TakeHomeAssignmentAPI.Services;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Serilog to log to a database
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read settings from appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true })
    .CreateLogger();

// ✅ Replace default logging with Serilog
builder.Host.UseSerilog();

// Load Configuration
var configuration = builder.Configuration;

// ✅ Configure Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// ✅ Configure Authentication using JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

// ✅ Register Repositories & Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPackagingService, PackagingService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ✅ Configure API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0); // Default to v1 if not specified
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Read version from URL segment
    options.UseApiBehavior = false; // Prevents automatic versioning errors
});


// ✅ Add Controllers
builder.Services.AddControllers();

// ✅ Enable Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TakeHomeAssignmentAPI", Version = "v1" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "TakeHomeAssignmentAPI", Version = "v2" });

    options.OperationFilter<SwaggerDefaultValues>(); // Add this to support multiple versions

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and then the JWT token."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


var app = builder.Build();

// ✅ Enable Serilog request logging middleware
app.UseSerilogRequestLogging();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is not null)
        {
            Log.Error(exceptionHandlerPathFeature.Error, "Unhandled exception occurred: {Message}", exceptionHandlerPathFeature.Error.Message);
        }

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"An internal server error occurred.\"}");
    });
});

// ✅ Middleware pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();