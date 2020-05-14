using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZLogger;

namespace Common.Kafka.Consumer
{
    internal sealed class BackgroundKafkaConsumer<TK, TV> : BackgroundService
    {
        private readonly KafkaConsumerConfig<TK, TV> _config;
        private readonly IKafkaHandler<TK, TV> _handler;
        private readonly ILogger<TK> _logger;

        public BackgroundKafkaConsumer(IOptions<KafkaConsumerConfig<TK, TV>> config, IKafkaHandler<TK, TV> handler, ILogger<TK> logger = null)
        {
            _handler = handler;
            _logger = logger;
            _config = config.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(
                () => ExecuteCore(stoppingToken),
                stoppingToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

        public void ExecuteCore(CancellationToken stoppingToken)
        {
            var builder = new ConsumerBuilder<TK, TV>(_config).SetValueDeserializer(new KafkaDeserializer<TV>());
            using (IConsumer<TK, TV> consumer = builder.Build())
            {
                if (_config.Active)
                {
                    consumer.Subscribe(_config.Topic);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        ConsumeResult<TK, TV> result = null;
                        try
                        {
                            result = consumer.Consume(TimeSpan.FromMilliseconds(1000));
                            if (result != null)
                            {
                                _handler.HandleAsync(result.Key, result.Value).GetAwaiter().GetResult();
                                consumer.StoreOffset(result);
                            } 
                        }
                        catch (Exception err)
                        {
                            _logger?.ZLogError(
                                "Ошибка при обработки сообщения из кафки: {0}\ntopic: {1}, partition: {2}, offset: {3}\ntrace:{4}", 
                                err.Message, _config.Topic, result?.Partition.Value, result?.Offset.Value, err.StackTrace);
                        }
                    }   
                }
            }
        }
    }
}