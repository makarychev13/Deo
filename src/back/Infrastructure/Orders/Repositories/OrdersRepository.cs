using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Dapper;
using Domain.Orders;
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
                    insert into Orders
                    (Title, Description, Link, Publication, FreelanceBurseId, Status)
                    values(@title, @description, @link, @publication, @freelanceBurseId, @status)";

                await connection.ExecuteAsync(query, orders.Select(p => new
                {
                    title = p.Body.Title,
                    description = p.Body.Description,
                    link = p.Body.Link,
                    publication = p.Body.Publication,
                    freelanceBurseId,
                    status = ProcessingStatus.New
                }));
            }
        }
    }
}