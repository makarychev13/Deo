using Infrastructure.Common.Database;
using Infrastructure.Orders.Repositories.FreelanceBurse;
using Infrastructure.Orders.Rss;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureStartupExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<OrdersReader>();
            services.AddSingleton<FreelanceBurseRepository>();
            services.AddSingleton<OrdersRepository>();
            services.AddSingleton<ISqlConnectionFactory>(p => new SqlConnectionFactory("Server=localhost;Database=postgres;User Id=postgres;Password=postgres"));

            return services;
        }
    }
}