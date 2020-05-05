﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Common.Repositories;
using Dapper;
using Domain.Notifications;
using Domain.Orders;
using Domain.Users;
using Domain.Users.ValueObjects;
using Migrations.Tables.Users;

namespace Infrastructure.Users.Repositories
{
    public sealed class UsersRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        
        public UsersRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User[]> GetForNotifications(Order order)
        {
            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                string query = $@"
                select distinct u.""TelegramId"", u.""Active"", u.""Email"", u.""Subscriptions""
                from ""Users"" u
                    join ""UsersToKeywords"" UTK on ""Users"".""Id"" = UTK.""UserId""
                    join ""Keywords"" k on UTK.""KeywordId"" = k.""Id""
                where
                    ""Active"" = true
                    and (""TelegramId"" is not null or ""Email"" is not null)";

                IEnumerable<UserEntity> result = await connection.QueryAsync<UserEntity>(query, new
                {
                    title = order.Body.Title,
                    description = order.Body.Description
                });

                return result.Select(p =>
                {
                    var contact = new Contact(p.TelegramId, p.Email);
                    return new User(p.Active, contact, p.Subscriptions);
                }).ToArray();
            }
        }
    }
}