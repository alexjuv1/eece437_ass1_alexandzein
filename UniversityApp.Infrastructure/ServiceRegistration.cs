using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Core.Interfaces;
using UniversityApp.Infrastructure.Persistence;
using UniversityApp.Infrastructure.Persistence.Repositories;

namespace UniversityApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}

