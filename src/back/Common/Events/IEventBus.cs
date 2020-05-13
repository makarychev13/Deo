using System.Threading.Tasks;

namespace Common.Events
{
    public interface IEventBus<TK, TV>
    {
        Task PublishAsync(TK key, TV message);
    }
}