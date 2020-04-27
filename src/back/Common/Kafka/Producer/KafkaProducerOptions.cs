namespace Common.Kafka.Producer
{
    public sealed class KafkaProducerOptions<Tk, Tv>
    {
        public string Topic { get; set; }
    }
}