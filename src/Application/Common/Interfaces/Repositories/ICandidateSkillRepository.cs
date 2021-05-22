using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICandidateSkillRepository
    {
        Task<int> AddAsync(CandidateSkill candidateSkill);
    }
}