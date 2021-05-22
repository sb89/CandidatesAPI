using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();

        Task<int> CreateAsync(Candidate candidate);

        Task<int> UpdateAsync(Candidate candidate);
    }
}