using Confluent.Kafka;

namespace Common.Kafka.Consumer
{
    public sealed class KafkaConsumerOptions
    {
        public string Topic { get; set; }
        public ConsumerConfig Config { get; set; }
    }
}