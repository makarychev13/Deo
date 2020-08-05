using System.Threading.Tasks;

using Common.Kafka.Producer;

namespace Common.Events
{
    internal sealed class EventBus<TK, TV> : IEventBus<TK, TV>
    {
        private readonly KafkaProducer<TK, TV> _producer;

        public EventBus(KafkaProducer<TK, TV> producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(TK key, TV message)
        {
            await _producer.ProduceAsync(key, message);
        }
    }
}