using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Orders.Rss.Reader
{
    public interface IRssReader
    {
        Task HandleNewOrdersAsync();
    }
}