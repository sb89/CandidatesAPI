using Infrastructure.Connection;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnectionFactory ConnectionFactory;

        protected BaseRepository(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }
    }
}