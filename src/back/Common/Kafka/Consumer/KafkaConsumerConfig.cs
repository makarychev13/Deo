using Confluent.Kafka;

namespace Common.Kafka.Consumer
{
    public sealed class KafkaConsumerConfig<TK, TV> : ConsumerConfig
    {
        public string Topic { get; set; }
    }
}