using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Infrastructure.Connection;

namespace Infrastructure.Repositories
{
    public class SkillRepository : BaseRepository, ISkillRepository
    {
        public SkillRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        
        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            using var conn = ConnectionFactory.GetConnection();

            const string sql = "SELECT * FROM Skill;";

            return await conn.QueryAsync<Skill>(sql);
        }
    }
}