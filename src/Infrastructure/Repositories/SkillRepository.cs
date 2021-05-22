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

        public async Task<IEnumerable<Skill>> GetSkillsForCandidateAsync(int candidateId)
        {
            using var conn = ConnectionFactory.GetConnection();

            const string sql = "SELECT s.* FROM Skill s INNER JOIN CandidateSkill cs ON s.Id = cs.SkillId WHERE cs.CandidateId = @CandidateId;";

            return await conn.QueryAsync<Skill>(sql, new { CandidateId = candidateId});
        }
    }
}