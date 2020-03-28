using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Domain.Orders.ValueObjects;

namespace Infrastructure.Orders.Rss.Reader
{
    public interface IOrdersReader
    {
        Task<OrderBody[]> GetUnhandledAsync();
        void Handle(IEnumerable<OrderBody> orders);
        OrderBody[] GetHandled();
    }
}