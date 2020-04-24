using System;
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
            if (!orders.Any())
            {
                return;
            }
            
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
                    with cte as (
                        update ""Orders""
                        set 
                            ""Status"" = '{ProcessingStatus.InProcess}', 
                            ""LastModificationDate"" = now()
                        where 
                            ""Status"" = '{ProcessingStatus.New}' 
                            or (""Status"" = '{ProcessingStatus.InProcess}' and date_part('minutes', now() - ""LastModificationDate""::timestamp) > 5)
                        returning ""Title"", ""Description"", ""Link"", ""Publication"", ""FreelanceBurseId""
                    )
                    select *
                    from cte
                    join ""FreelanceBurses"" fb on cte.""FreelanceBurseId"" = fb.""Id""
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

        public async Task HandleOrders(IEnumerable<Uri> links)
        {
            if (!links.Any())
            {
                return;
            }
            
            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                string query = $@"
                    update ""Orders""
                    set ""Status"" = '{ProcessingStatus.Finish}'
                    where ""Link"" = ANY(@links)
                ";

                await connection.ExecuteAsync(query, 
                    new
                    {
                        links = links.Select(p => p.ToString()).ToArray()
                    });
            }
        }
    }
}