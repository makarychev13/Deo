using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
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
            return Task.Factory.StartNew(
                () => ExecuteCore(stoppingToken),
                stoppingToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

        protected void ExecuteCore(CancellationToken stoppingToken)
        {
            using (IConsumer<TK, TV> consumer = new ConsumerBuilder<TK, TV>(_options.Config).Build())
            {
                consumer.Subscribe(_options.Topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    ConsumeResult<TK, TV> result = consumer.Consume(TimeSpan.FromMilliseconds(100));
                    if (NeedConsume(result))
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