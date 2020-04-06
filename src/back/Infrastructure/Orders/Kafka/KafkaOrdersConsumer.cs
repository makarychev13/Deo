using System;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Orders;

namespace Infrastructure.Orders.Kafka
{
    public sealed class KafkaOrdersConsumer : KafkaConsumer<string, Order>
    {
        public KafkaOrdersConsumer(ConsumerConfig options) : base(options)
        {
        }

        protected override string Topic => "orders";
        
        protected override async Task ConsumeAsync(string key, Order message)
        {
            await Task.CompletedTask;
            Console.WriteLine(message.Body.Title);
        }

        protected override bool NeedConsume(string key, Order message)
        {
            return true;
        }
    }
}