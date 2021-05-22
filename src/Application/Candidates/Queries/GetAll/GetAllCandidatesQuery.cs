using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Candidates.Queries.GetAll
{
    public class GetAllCandidatesQuery : IRequest<GetAllCandidatesVm>
    {
        
    }

    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, GetAllCandidatesVm>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public GetAllCandidatesQueryHandler(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }
        
        public async Task<GetAllCandidatesVm> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetAllAsync();

            return new GetAllCandidatesVm
            {
                Candidates = _mapper.Map<IEnumerable<CandidateDto>>(candidates)
            };
        }
    }
}