using System;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Infrastructure.Connection;

namespace Infrastructure.Repositories
{
    public class CandidateSkillRepository : BaseRepository, ICandidateSkillRepository
    {
        public CandidateSkillRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<CandidateSkill> GetAsync(int candidateId, int skillId)
        {
            using var conn = ConnectionFactory.GetConnection();
            
            const string sql = @"SELECT * FROM CandidateSkill WHERE CandidateID = @CandidateId AND SkillID = @SkillId";

            return await conn.QueryFirstOrDefaultAsync<CandidateSkill>(sql,
                new {CandidateId = candidateId, SkillId = skillId});
        }

        public async Task<int> AddAsync(CandidateSkill candidateSkill)
        {
            using var conn = ConnectionFactory.GetConnection();

            candidateSkill.UpdatedDate = candidateSkill.CreatedDate = DateTimeOffset.Now;
            
            const string sql = @"INSERT INTO CandidateSkill(ID, CandidateID, CreatedDate, UpdatedDate, SkillID)
                      VALUES ((SELECT ISNULL(MAX(ID) + 1, 0) FROM CandidateSkill), @CandidateId, @CreatedDate, @UpdatedDate, 
                              @SkillId); SELECT MAX(ID) FROM CandidateSkill;";

            return await conn.ExecuteScalarAsync<int>(sql, candidateSkill);
        }

        public async Task DeleteAsync(int candidateId, int skillId)
        {
            using var conn = ConnectionFactory.GetConnection();

            const string sql = "DELETE FROM CandidateSkill WHERE CandidateID = @CandidateId AND SkillID = @SkillId";

            await conn.ExecuteAsync(sql, new {CandidateId = candidateId, SkillId = skillId});
        }
    }
}