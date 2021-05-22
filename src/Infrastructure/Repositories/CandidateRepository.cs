using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Connection;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : BaseRepository, ICandidateRepository
    {
        public CandidateRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            using var conn = ConnectionFactory.GetConnection();

            const string sql = "SELECT * FROM Candidate;";

            return await conn.QueryAsync<Candidate>(sql);
        }
    }
}