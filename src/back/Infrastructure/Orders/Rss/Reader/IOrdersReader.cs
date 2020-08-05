using System.Threading.Tasks;

using Domain.Orders;
using Domain.Orders.ValueObjects;

namespace Infrastructure.Orders.Rss.Reader
{
    public interface IOrdersReader
    {
        Task<Order[]> GetFrom(FreelanceBurse burse);
    }
}