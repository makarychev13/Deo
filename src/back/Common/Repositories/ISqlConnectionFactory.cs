using System.Data;

namespace Common.Repositories
{
    public interface ISqlConnectionFactory
    {
        IDbConnection BuildConnection();
    }
}