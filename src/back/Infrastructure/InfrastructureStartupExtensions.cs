using Infrastructure.Notifications;
using Infrastructure.Notifications.Repositories;
using Infrastructure.Orders.Repositories;
using Infrastructure.Orders.Rss.Parser;
using Infrastructure.Orders.Rss.Reader;
using Infrastructure.Users.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureStartupExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IOrdersParser, OrdersParser>();
            services.AddSingleton<IOrdersReader, OrdersReader>();
            services.AddSingleton<OrdersRepository>();
            services.AddSingleton<FreelanceBursesRepository>();
            services.AddSingleton<UsersRepository>();
            services.AddSingleton<NotificationsFabric>();
            services.AddSingleton<OutboxNotificationsRepository>();
            
            services.AddEventBus();

            return services;
        }
    }
}