using System;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Orders;

namespace Infrastructure.Orders.Kafka
{
    public sealed class KafkaOrdersConsumer : KafkaConsumer<string, Order>
    {
        public KafkaOrdersConsumer(KafkaConsumerOptions options) : base(options)
        {
        }

        protected override async Task ConsumeAsync(ConsumeResult<string, Order> message)
        {
            await Task.CompletedTask;
            Console.WriteLine(message.Value.Body.Title);
        }

        protected override bool NeedConsume(ConsumeResult<string, Order> message)
        {
            return true;
        }
    }
}