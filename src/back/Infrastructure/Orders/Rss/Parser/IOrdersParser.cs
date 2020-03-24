﻿using System.Collections.Generic;
using System.Xml.Linq;

using Domain.Orders.ValueObjects;

namespace Infrastructure.Orders.Rss.Parser
{
    public interface IOrdersParser
    {
        List<OrderBody> GetFrom(XDocument xml);
        XDocument ToXml(IEnumerable<OrderBody> orders);
    }
}