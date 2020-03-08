using System;
using System.Threading.Tasks;
using Infrastructure.Orders.Rss.Reader;

namespace DomainServices.Orders
{
    public sealed class OrdersService
    {
        public readonly IRssReader _reader;

        public OrdersService(IRssReader reader)
        {
            _reader = reader;
        }

        public async Task HandleOrdersAsync()
        {
            using (var mutex = _reader.GetProccesingLock())
            {
                mutex.WaitOne(TimeSpan.FromMinutes(1));
                var orders = await _reader.GetNewItemAsync();
                //saveToKafka()
                await _reader.UpdateOldItems(orders);
                mutex.ReleaseMutex();
            }
        } 
    }
}