using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Common.Repositories;
using Domain.Notifications;
using Domain.Orders;
using Domain.Users.ValueObjects;

namespace Infrastructure.Users.Repositories
{
    public sealed class UsersRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        
        public UsersRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Dictionary<Subscriptions, Contact[]>> GetForNotifications(Order order)
        {
            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                string query = $@"";
            }
            throw new NotImplementedException();
        }
    }
}