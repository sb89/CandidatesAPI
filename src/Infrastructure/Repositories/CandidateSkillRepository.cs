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

        public async Task<int> AddAsync(CandidateSkill candidateSkill)
        {
            using var conn = ConnectionFactory.GetConnection();

            candidateSkill.UpdatedDate = candidateSkill.CreatedDate = DateTimeOffset.Now;
            
            const string sql = @"INSERT INTO CandidateSkill(ID, CandidateID, CreatedDate, UpdatedDate, SkillID)
                      VALUES ((SELECT ISNULL(MAX(ID) + 1, 0) FROM CandidateSkill), @CandidateId, @CreatedDate, @UpdatedDate, 
                              @SkillId); SELECT MAX(ID) FROM CandidateSkill;";

            return await conn.ExecuteScalarAsync<int>(sql, candidateSkill);
        }
    }
}