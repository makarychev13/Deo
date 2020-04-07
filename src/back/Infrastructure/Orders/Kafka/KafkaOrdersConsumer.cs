using System;
using System.Threading.Tasks;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Orders;
using Domain.Users;
using Infrastructure.Users.Repositories;

namespace Infrastructure.Orders.Kafka
{
    public sealed class KafkaOrdersConsumer : KafkaConsumer<string, Order>
    {
        private readonly UsersRepository _usersRepository;
        
        public KafkaOrdersConsumer(ConsumerConfig options, UsersRepository usersRepository) : base(options)
        {
            _usersRepository = usersRepository;
        }

        protected override string Topic => "orders";
        
        protected override async Task ConsumeAsync(string key, Order message)
        {
            User[] users = await _usersRepository.GetForNotifications(message);
            Console.WriteLine(message.Body.Title);
        }

        protected override bool NeedConsume(string key, Order message)
        {
            return true;
        }
    }
}