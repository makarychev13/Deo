using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Orders.Rss.Reader
{
    public interface IOrdersReader
    {
        Task<Order[]> GetNewAsync();
        void UpdateOld(IEnumerable<Order> orders);
        Mutex GetProccesLock();
    }
}