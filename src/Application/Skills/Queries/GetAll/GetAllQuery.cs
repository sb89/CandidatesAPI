using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Skills.Queries.GetAll
{
    public class GetAllQuery : IRequest<GetAllVm>
    {
        
    }
    
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetAllVm>
    {
        private readonly IMapper _mapper;
        private readonly ISkillRepository _repository;

        public GetAllQueryHandler(IMapper mapper, ISkillRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        public async Task<GetAllVm> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _repository.GetAllAsync();

            return new GetAllVm
            {
                Skills = _mapper.Map<IEnumerable<SkillDto>>(candidates)
            };
        }
    }
}