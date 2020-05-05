using Common.Kafka;
using Common.Kafka.Consumer;
using Common.Kafka.Producer;
using Confluent.Kafka;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KafkaStartupExtensions
    {
        public static IServiceCollection AddKafkaConsumer<TK, TV, THandler>(this IServiceCollection services)
            where THandler : class, IKafkaHandler<TK, TV>
        {
            services.AddSingleton<IKafkaHandler<TK, TV>, THandler>();
            services.AddHostedService<BackgroundKafkaConsumer<TK, TV>>();

            return services;
        }

        public static IServiceCollection AddKafkaProducer<TK, TV>(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<KafkaProducerConfig<TK, TV>>();
                var builder = new ProducerBuilder<TK, TV>(config)
                    .SetValueSerializer(new KafkaSerializer<TV>());
                
                return builder.Build();
            });

            services.AddSingleton<KafkaProducer<TK, TV>>();

            return services;
        }
    }
}