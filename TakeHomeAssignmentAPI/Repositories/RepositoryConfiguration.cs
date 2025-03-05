namespace TakeHomeAssignmentAPI.Repositories
{
    public static class RepositoryConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPackagingRepository, PackagingRepository>(); // Add more repositories as needed
        }
    }
}
