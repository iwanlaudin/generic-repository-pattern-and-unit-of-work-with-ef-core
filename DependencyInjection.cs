using GenericRepositoryPattern.Abstractions;
using Microsoft.EntityFrameworkCore;
using GenericRepositoryPattern.Repositories;

namespace GenericRepositoryPattern
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration, string connectionString = "DefaultConnection")
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseNpgsql(configuration.GetConnectionString(connectionString)));

            services.AddScoped<IApplicationDbContext>(provider => 
                provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}