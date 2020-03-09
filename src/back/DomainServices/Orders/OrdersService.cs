using System;
using System.Threading.Tasks;
using Infrastructure.Orders.Rss.Reader;

namespace DomainServices.Orders
{
    public sealed class OrdersService
    {
        private readonly IOrdersReader _reader;

        public OrdersService(IOrdersReader reader)
        {
            _reader = reader;
        }

        public async Task HandleOrdersAsync()
        {
            using (var mutex = _reader.GetProccesLock())
            {
                mutex.WaitOne(TimeSpan.FromMinutes(1));
                var orders = await _reader.GetNewAsync();
                //saveToKafka(orders)
                _reader.UpdateOld(orders);
                mutex.ReleaseMutex();
            }
        } 
    }
}