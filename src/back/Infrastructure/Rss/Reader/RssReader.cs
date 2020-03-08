using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Rss.Parser;

namespace Infrastructure.Rss.Reader
{
    public class RssReader : IRssReader
    {
        private readonly Uri _link;
        private readonly IRssParser _parser;
        private readonly string _fileName;

        public RssReader(Uri link, IRssParser parser, string fileName)
        {
            _link = link;
            _parser = parser;
            _fileName = fileName;
        }

        public Task<Order[]> GetNewOrdersAsync()
        {
            throw new NotImplementedException();
        }
    }
}