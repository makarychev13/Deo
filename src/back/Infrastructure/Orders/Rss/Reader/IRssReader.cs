using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Orders.Rss.Reader
{
    public interface IRssReader
    {
        Task<Order[]> GetNewItemAsync();
        Task UpdateOldItems(IEnumerable<Order> oders);
        Mutex GetProccesingLock();
    }
}