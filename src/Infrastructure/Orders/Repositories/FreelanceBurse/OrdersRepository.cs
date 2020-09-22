using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Domain.Orders;

using Infrastructure.Common.Database;
using Infrastructure.Orders.Models;

namespace Infrastructure.Orders.Repositories.FreelanceBurse
{
    public sealed class OrdersRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public OrdersRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Merge(IEnumerable<Order> orders, int freelanceBurseId, CancellationToken token)
        {
            using IDbConnection connection = _connectionFactory.BuildConnection();

            var query = @"
                    insert into ""Orders""
                    (""Title"", ""Description"", ""Link"", ""Publication"", ""FreelanceBurseId"", ""Status"")
                    values(@title, @description, @link, @publication, @freelanceBurseId, @status)
                    on conflict (""Link"") do nothing";

            await connection.ExecuteAsync(
                query,
                orders.Select(
                    p => new
                    {
                        title = p.Body.Title,
                        description = p.Body.Description,
                        link = p.Body.Link.ToString(),
                        publication = p.Body.Publication,
                        freelanceBurseId,
                        status = ProcessingStatusEntity.New.ToString()
                    }));
        }
    }
}