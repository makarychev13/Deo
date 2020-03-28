using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Infrastructure.Common.Kafka
{
    public sealed class KafkaProducer<Tk, Tv> : IDisposable
    {
        private readonly string _topic;
        private readonly IProducer<Tk, Tv> _producer;

        public KafkaProducer(IOptions<KafkaProducerOptions> topicOptions, IProducer<Tk, Tv> producer)
        {
            _topic = topicOptions.Value.Topic;
            _producer = producer;
        }

        public async Task ProduceAsync(Tk key, Tv value)
        {
            await _producer.ProduceAsync(_topic, new Message<Tk, Tv>() {Key = key, Value = value});
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}