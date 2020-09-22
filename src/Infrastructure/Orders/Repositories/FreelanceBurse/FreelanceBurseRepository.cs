using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Dapper;

using Infrastructure.Common.Database;
using Infrastructure.Orders.Models;

namespace Infrastructure.Orders.Repositories.FreelanceBurse
{
    public sealed class FreelanceBurseRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public FreelanceBurseRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Domain.Orders.FreelanceBurse>> GetAll()
        {
            using IDbConnection connection = _connectionFactory.BuildConnection();

            var query = @"select ""Id"", ""Name"", ""Link"" from ""FreelanceBurses""";

            IEnumerable<FreelanceBurseEntity> result = await connection.QueryAsync<FreelanceBurseEntity>(query);

            return result.Select(p => new Domain.Orders.FreelanceBurse(p.Id, new Uri(p.Link), p.Name)).ToArray();
        }
    }
}