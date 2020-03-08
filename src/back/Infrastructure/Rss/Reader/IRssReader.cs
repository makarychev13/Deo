using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Rss.Reader
{
    public interface IRssReader
    {
        Task<Order[]> GetNewOrdersAsync();
    }
}