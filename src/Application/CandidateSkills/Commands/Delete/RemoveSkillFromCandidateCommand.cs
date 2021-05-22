using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.CandidateSkills.Commands.Delete
{
    public class RemoveSkillFromCandidateCommand : IRequest
    {
        public int CandidateId { get; set; }

        public int SkillId { get; set; }
    }
    
    public class RemoveSkillFromCandidateCommandHandler : IRequestHandler<RemoveSkillFromCandidateCommand>
    {
        private readonly ICandidateSkillRepository _candidateSkillRepository;

        public RemoveSkillFromCandidateCommandHandler(ICandidateSkillRepository candidateSkillRepository)
        {
            _candidateSkillRepository = candidateSkillRepository;
        }
        
        public async Task<Unit> Handle(RemoveSkillFromCandidateCommand request, CancellationToken cancellationToken)
        {
            await _candidateSkillRepository.DeleteAsync(request.CandidateId, request.SkillId);
            
            return Unit.Value;
        }
    }
}