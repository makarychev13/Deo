﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Dapper;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using Migrations.Tables.FreelanceBurses;
using Migrations.Tables.Orders;

namespace Infrastructure.Orders.Repositories
{
    public sealed class OrdersRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public OrdersRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task MergePulledOrdersAsync(IEnumerable<Order> orders, int freelanceBurseId)
        {
            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                string query = $@"
                    insert into ""Orders""
                    (""Title"", ""Description"", ""Link"", ""Publication"", ""FreelanceBurseId"", ""Status"")
                    values(@title, @description, @link, @publication, @freelanceBurseId, @status)
                    on conflict (""Link"") do nothing";

                await connection.ExecuteAsync(query, orders.Select(p => new
                {
                    title = p.Body.Title,
                    description = p.Body.Description,
                    link = p.Body.Link.ToString(),
                    publication = p.Body.Publication,
                    freelanceBurseId,
                    status = ProcessingStatus.New.ToString()
                }));
            }
        }

        public async Task<Order[]> GetUnhandledOrdersAsync()
        {
            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                string query = $@"
                    update ""Orders""
                    set ""Status"" = '{ProcessingStatus.InProcess.ToString()}'
                    from (
                        select
                            o.""Id"", 
                            o.""Title"", 
                            o.""Description"", 
                            o.""Link"", 
                            o.""Publication"", 
                            o.""FreelanceBurseId"",
                            fb.""Id"",
                            fb.""Link"", 
                            fb.""Name""
                        from ""Orders"" o
                        join ""FreelanceBurses"" fb on o.""FreelanceBurseId"" = fb.""Id""
                        where o.""Status"" = '{ProcessingStatus.New.ToString()}'
                    ) as result
                    returning result.*
                ";

                var result = await connection.QueryAsync<OrderEntity, FreelanceBurseEntity, OrderEntity>(query, (o, fb) =>
                    {
                        o.FreelanceBurse = fb;
                        return o;
                    });

                return result.Select(p =>
                {
                    var body = new OrderBody(p.Title, p.Description, new Uri(p.Link), p.Publication);
                    var burse = new FreelanceBurse(p.FreelanceBurseId, new Uri(p.FreelanceBurse.Link), p.FreelanceBurse.Name);
                    return new Order(body, burse);
                }).ToArray();
            }
        }
    }
}