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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ExecuteCore(stoppingToken);
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
                    if (result != null && NeedConsume(result))
                    {
                        ConsumeAsync(result).GetAwaiter().GetResult();
                        consumer.StoreOffset(result);
                    }
                }
            }
        }

        protected abstract Task ConsumeAsync(ConsumeResult<TK, TV> message);

        protected abstract bool NeedConsume(ConsumeResult<TK, TV> message);
    }
}