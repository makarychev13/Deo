using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Common.Kafka.Consumer
{
    internal sealed class BackgroundKafkaConsumer<TK, TV> : BackgroundService
    {
        private readonly KafkaConsumerConfig<TK, TV> _config;
        private readonly IKafkaHandler<TK, TV> _handler;

        public BackgroundKafkaConsumer(IOptions<KafkaConsumerConfig<TK, TV>> config, IKafkaHandler<TK, TV> handler)
        {
            _handler = handler;
            _config = config.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(
                () => ExecuteCore(stoppingToken),
                stoppingToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current).ContinueWith(p => { throw p.Exception; }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public void ExecuteCore(CancellationToken stoppingToken)
        {
            var builder = new ConsumerBuilder<TK, TV>(_config).SetValueDeserializer(new KafkaDeserializer<TV>());
            using (IConsumer<TK, TV> consumer = builder.Build())
            {
                consumer.Subscribe(_config.Topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    ConsumeResult<TK, TV> result = consumer.Consume(TimeSpan.FromMilliseconds(1000));
                    if (result != null)
                    {
                        _handler.HandleAsync(result.Key, result.Value).GetAwaiter().GetResult();
                        consumer.StoreOffset(result);
                    }
                }
            }
        }
    }
}