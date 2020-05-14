using Confluent.Kafka;

namespace Common.Kafka.Consumer
{
    public sealed class KafkaConsumerConfig<TK, TV> : ConsumerConfig
    {
        public KafkaConsumerConfig()
        {
            AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
            EnableAutoOffsetStore = false;
            EnableAutoCommit = true;
        }

        public string Topic { get; set; }
        public bool Active { get; set; } = true;
    }
}