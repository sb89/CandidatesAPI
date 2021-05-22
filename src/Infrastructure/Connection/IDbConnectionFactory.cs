using System.Data;

namespace Infrastructure.Connection
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}