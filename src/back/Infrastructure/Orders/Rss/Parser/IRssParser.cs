using System;
using System.Threading.Tasks;

namespace Infrastructure.Orders.Rss.Parser
{
    public interface IRssParser
    {
        Task<Order[]> GetFromAsync(Uri link);
    }
}