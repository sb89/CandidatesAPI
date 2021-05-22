using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICandidateSkillRepository
    {
        Task<CandidateSkill> GetAsync(int candidateId, int skillId);
        
        Task<int> AddAsync(CandidateSkill candidateSkill);
    }
}