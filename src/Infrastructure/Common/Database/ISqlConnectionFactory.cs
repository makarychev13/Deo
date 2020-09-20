using System.Data;

namespace Infrastructure.Common.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection BuildConnection();
    }
}