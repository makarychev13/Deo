using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Orders.Rss.Parser
{
    public interface IRssParser
    {
        List<Order> GetFrom(XDocument xml);
    }
}