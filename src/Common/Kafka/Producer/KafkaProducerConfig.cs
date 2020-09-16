using Confluent.Kafka;

namespace Common.Kafka.Producer
{
    public sealed class KafkaProducerConfig<Tk, Tv> : ProducerConfig
    {
        public string Topic { get; set; }
    }
}