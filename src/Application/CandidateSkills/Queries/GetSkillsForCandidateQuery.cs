using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CandidateSkills.Queries
{
    public class GetSkillsForCandidateQuery : IRequest<GetSkillsForCandidateVm>
    {
        public int CandidateId { get; set; }
    }

    public class GetSkillsForCandidateQueryHandler : IRequestHandler<GetSkillsForCandidateQuery, GetSkillsForCandidateVm>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetSkillsForCandidateQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        
        public async Task<GetSkillsForCandidateVm> Handle(GetSkillsForCandidateQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetSkillsForCandidateAsync(request.CandidateId);

            return new GetSkillsForCandidateVm
            {
                Skills = _mapper.Map<IEnumerable<SkillDto>>(skills)
            };
        }
    }
}