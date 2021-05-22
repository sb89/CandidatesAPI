using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface ISkillRepository
    {
        Task<Skill> GetAsync(int id);
        
        Task<IEnumerable<Skill>> GetAllAsync();

        Task<IEnumerable<Skill>> GetSkillsForCandidateAsync(int userId);
    }
}