using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Microsoft.Extensions.Hosting;

namespace Common.Kafka.Consumer
{
    public abstract class KafkaConsumer<TK, TV> : BackgroundService
    {
        private readonly KafkaConsumerOptions _options;

        protected KafkaConsumer(KafkaConsumerOptions options)
        {
            _options = options;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ExecuteCore(stoppingToken);
            return Task.CompletedTask;
        }

        protected void ExecuteCore(CancellationToken stoppingToken)
        {
            var builder = new ConsumerBuilder<TK, TV>(_options.Config).SetValueDeserializer(new KafkaDeserializer<TV>());
            using (IConsumer<TK, TV> consumer = builder.Build())
            {
                consumer.Subscribe(_options.Topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    ConsumeResult<TK, TV> result = consumer.Consume(TimeSpan.FromMilliseconds(1000));
                    if (result != null && NeedConsume(result.Key, result.Value))
                    {
                        ConsumeAsync(result.Key, result.Value).GetAwaiter().GetResult();
                        consumer.StoreOffset(result);
                    }
                }
            }
        }

        protected abstract Task ConsumeAsync(TK key, TV message);

        protected abstract bool NeedConsume(TK key, TV message);
    }
}