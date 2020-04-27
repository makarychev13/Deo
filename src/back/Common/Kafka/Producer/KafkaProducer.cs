using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Common.Kafka.Producer
{
    public sealed class KafkaProducer<Tk, Tv> : IDisposable
    {
        private readonly IProducer<Tk, Tv> _producer;
        private readonly string _topic;

        public KafkaProducer(IOptions<KafkaProducerOptions<Tk, Tv>> topicOptions, IProducer<Tk, Tv> producer)
        {
            _topic = topicOptions.Value.Topic;
            _producer = producer;
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task ProduceAsync(Tk key, Tv value)
        {
            await _producer.ProduceAsync(_topic, new Message<Tk, Tv> {Key = key, Value = value});
        }
    }
}