using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Candidates.Queries.GetAll
{
    public class GetAllQuery : IRequest<GetAllVm>
    {
        
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, GetAllVm>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }
        
        public async Task<GetAllVm> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetAll();

            return new GetAllVm
            {
                Candidates = _mapper.Map<IEnumerable<CandidateDto>>(candidates)
            };
        }
    }
}