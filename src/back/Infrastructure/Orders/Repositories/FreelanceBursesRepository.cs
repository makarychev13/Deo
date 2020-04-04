using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Dapper;
using Domain.Orders.ValueObjects;
using Migrations.Tables.FreelanceBurses;

namespace Infrastructure.Orders.Repositories
{
    public sealed class FreelanceBursesRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public FreelanceBursesRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<FreelanceBurse[]> GetAll()
        {
            using (IDbConnection connection = _connectionFactory.BuildConnection())
            {
                string query = $@"
                    select ""Id"", ""Name"", ""Link""
                    from ""FreelanceBurses""";

                var result = await connection.QueryAsync<FreelanceBurseEntity>(query);
                return result.Select(p => new FreelanceBurse(p.Id, new Uri(p.Link), p.Name)).ToArray();
            }
        }
    }
}