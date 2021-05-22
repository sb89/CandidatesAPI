using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Skills.Queries.GetAll
{
    public class GetAllSkillsQuery : IRequest<GetAllSkillsVm>
    {
        
    }
    
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, GetAllSkillsVm>
    {
        private readonly IMapper _mapper;
        private readonly ISkillRepository _repository;

        public GetAllSkillsQueryHandler(IMapper mapper, ISkillRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        public async Task<GetAllSkillsVm> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _repository.GetAllAsync();

            return new GetAllSkillsVm
            {
                Skills = _mapper.Map<IEnumerable<SkillDto>>(candidates)
            };
        }
    }
}