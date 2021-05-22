using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
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
        private readonly ICandidateSkillRepository _candidateSkillRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public AddSkillToCandidateCommandHandler(ICandidateSkillRepository candidateSkillRepository, 
            ISkillRepository skillRepository, ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateSkillRepository = candidateSkillRepository;
            _skillRepository = skillRepository;
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(AddSkillToCandidateCommand request, CancellationToken cancellationToken)
        {

            if (await _skillRepository.GetAsync(request.SkillId) == null)
            {
                throw new BadRequestException("Skill does not exist");
            }
            
            if (await _candidateRepository.GetAsync(request.CandidateId) == null)
            {
                throw new BadRequestException("Candidate does not exist");
            }

            var existing = await _candidateSkillRepository.GetAsync(request.CandidateId, request.SkillId);
            if (existing != null)
            {
                throw new BadRequestException("Skill already assigned to Candidate.");
            }

            var candidateSkill = _mapper.Map<CandidateSkill>(request);

            return await _candidateSkillRepository.AddAsync(candidateSkill);
        }
    }
}