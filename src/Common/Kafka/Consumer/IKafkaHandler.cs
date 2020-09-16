using System.Threading.Tasks;

namespace Common.Kafka.Consumer
{
    public interface IKafkaHandler<TK, TV>
    {
        Task HandleAsync(TK key, TV value);
    }
}