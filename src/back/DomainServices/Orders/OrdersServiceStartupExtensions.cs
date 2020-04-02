using Common.Kafka.Producer;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using DomainServices.Orders;
using Infrastructure.Orders.Rss.Parser;
using Infrastructure.Orders.Rss.Reader;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OrdersServiceStartupExtensions
    {
        public static IServiceCollection AddOrdersService(this IServiceCollection services, FreelanceBurse burse, string fileName)
        {
            var parser = new OrdersParser();
            var reader = new OrdersReader(parser, burse, fileName);
            services.AddSingleton<IHostedService>(sp =>
            {
                var producer = sp.GetRequiredService<KafkaProducer<string, Order>>();
                return new OrdersService(reader, producer);
            });
            
            return services;
        }
    }
}