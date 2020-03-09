using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Orders.Rss.Reader
{
    public interface IOrdersReader
    {
        Task<Order[]> GetNewAsync();
        Task UpdateOld(IEnumerable<Order> oders);
        Mutex GetProccesLock();
    }
}