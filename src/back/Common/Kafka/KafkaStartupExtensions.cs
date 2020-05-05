using Common.Kafka;
using Common.Kafka.Consumer;
using Common.Kafka.Producer;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

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
            services.AddConfluentKafkaProducer<TK, TV>();
            services.AddSingleton<KafkaProducer<TK, TV>>();

            return services;
        }

        public static IServiceCollection AddKafkaProducer<TK, TV, THandler>(this IServiceCollection services)
            where THandler : KafkaProducer<TK, TV>
        {
            services.AddConfluentKafkaProducer<TK, TV>();
            services.AddSingleton<KafkaProducer<TK, TV>, THandler>();
            
            return services;
        }

        private static IServiceCollection AddConfluentKafkaProducer<TK, TV>(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IOptions<KafkaProducerConfig<TK, TV>>>();
                var builder = new ProducerBuilder<TK, TV>(config.Value)
                    .SetValueSerializer(new KafkaSerializer<TV>());
                
                return builder.Build();
            });

            return services;
        }
    }
}