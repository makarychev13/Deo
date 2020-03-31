using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Common.Kafka
{
    public abstract class KafkaConsumer<TK, TV> : BackgroundService
    {
        private readonly ConsumerConfig _config;
        private readonly string _topic;

        protected KafkaConsumer(ConsumerConfig config, string topic)
        {
            _config = config;
            _topic = topic;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(
                async () => await ExecuteCore(stoppingToken),
                stoppingToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

        protected async Task ExecuteCore(CancellationToken stoppingToken)
        {
            using (IConsumer<TK, TV> consumer = new ConsumerBuilder<TK, TV>(_config).Build())
            {
                consumer.Subscribe(_topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    ConsumeResult<TK, TV> result = consumer.Consume(TimeSpan.FromMilliseconds(100));
                    if (NeedConsume(result))
                    {
                        await ConsumeAsync(result);
                        consumer.StoreOffset(result);
                    }
                }
            }
        }

        protected abstract Task ConsumeAsync(ConsumeResult<TK, TV> message);

        protected abstract bool NeedConsume(ConsumeResult<TK, TV> message);
    }
}