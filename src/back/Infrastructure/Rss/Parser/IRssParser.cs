using System;
using System.Threading.Tasks;

namespace Infrastructure.Rss.Parser
{
    public interface IRssParser
    {
        Task<Order[]> GetFromAsync(Uri link);
    }
}