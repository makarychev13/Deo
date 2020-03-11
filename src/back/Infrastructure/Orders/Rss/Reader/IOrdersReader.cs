using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Orders.Rss.Reader
{
    public interface IOrdersReader
    {
        Task<Order[]> GetUnhandledAsync();
        void Handle(IEnumerable<Order> orders);
        Order[] GetHandled();
        Mutex GetProccesLock();
    }
}