using System.Data;

using Npgsql;

namespace Common.Repositories
{
    public sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection BuildConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}