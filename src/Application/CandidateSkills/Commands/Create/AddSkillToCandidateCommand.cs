using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CandidateSkills.Commands.Create
{
    public class AddSkillToCandidateCommand : IRequest<int>
    {
        public int CandidateId { get; set; }

        public int SkillId { get; set; }
    }

    public class AddSkillToCandidateCommandHandler : IRequestHandler<AddSkillToCandidateCommand, int>
    {
        private readonly ICandidateSkillRepository _repository;
        private readonly IMapper _mapper;

        public AddSkillToCandidateCommandHandler(ICandidateSkillRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(AddSkillToCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidateSkill = _mapper.Map<CandidateSkill>(request);

            return await _repository.AddAsync(candidateSkill);
        }
    }
}