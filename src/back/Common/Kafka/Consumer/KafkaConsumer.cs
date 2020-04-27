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
        private readonly ConsumerConfig _options;

        protected KafkaConsumer(ConsumerConfig options)
        {
            _options = options;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(
                () => ExecuteCore(stoppingToken),
                stoppingToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current).ContinueWith(p => { throw p.Exception; }, TaskContinuationOptions.OnlyOnFaulted);
        }

        protected void ExecuteCore(CancellationToken stoppingToken)
        {
            var builder = new ConsumerBuilder<TK, TV>(_options).SetValueDeserializer(new KafkaDeserializer<TV>());
            using (IConsumer<TK, TV> consumer = builder.Build())
            {
                consumer.Subscribe(Topic);

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

        protected abstract string Topic { get; }
    }
}