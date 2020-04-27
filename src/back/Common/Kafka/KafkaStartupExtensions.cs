using Common.Kafka;
using Common.Kafka.Producer;
using Confluent.Kafka;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class KafkaStartupExtensions
    {
        public static IServiceCollection AddKafkaConfigs(this IServiceCollection services,
            ProducerConfig producerConfig)
        {
            services.AddSingleton(producerConfig);

            return services;
        }

        public static IServiceCollection AddKafkaProducer<Tk, Tv>(this IServiceCollection services, string topic)
        {
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<ProducerConfig>();
                var builder = new ProducerBuilder<Tk, Tv>(config)
                    .SetValueSerializer(new KafkaSerializer<Tv>())
                    .SetKeySerializer(new KafkaSerializer<Tk>());

                return builder.Build();
            });

            services
                .Configure<KafkaProducerOptions<Tk, Tv>>(p => p.Topic = topic)
                .AddSingleton<KafkaProducer<Tk, Tv>>();

            return services;
        }
    }
}