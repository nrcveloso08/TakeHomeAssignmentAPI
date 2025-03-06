using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TakeHomeAssignmentAPI.Models;
using TakeHomeAssignmentAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;

namespace TakeHomeAssignmentAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _userService = userService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _userService.UserExistsAsync(request.Email))
                return BadRequest(new { Message = "Email already exists." });

            // Generate Two-Factor Authentication codes
            var twoFactorCode = GenerateTwoFactorCode();
            var twoFactorRecoveryCode = GenerateRecoveryCode();

            var success = await _userService.CreateUserAsync(request.Email, request.Password, twoFactorCode, twoFactorRecoveryCode);

            if (!success)
                return StatusCode(500, new { Message = "Failed to create user." });

            return Ok(new
            {
                Message = "User registered successfully!",
                TwoFactorCode = twoFactorCode,
                TwoFactorRecoveryCode = twoFactorRecoveryCode
            });
        }

        // Helper methods for 2FA
        private string GenerateTwoFactorCode()
        {
            return new Random().Next(100000, 999999).ToString(); 
        }

        private string GenerateRecoveryCode()
        {
            return Guid.NewGuid().ToString(); 
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterRequest request)
        {
            var user = await _userService.ValidateUserAsync(request.Email, request.Password);
            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials." });

            var token = GenerateJwtToken(user);
            _logger.LogInformation("User {Username} successfully authenticated.", request.Email);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["Jwt:Key"];

            // Ensure the key is valid
            if (string.IsNullOrEmpty(secretKey) || secretKey.Length < 32)
                throw new Exception("JWT Key is too short. It must be at least 32 characters.");

            var key = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(key);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
