using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Infrastructure.Persistence;

namespace PetFamily.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure (
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Database"));
                options.UseSnakeCaseNamingConvention();
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            });

            return services;
        }
    }
}
