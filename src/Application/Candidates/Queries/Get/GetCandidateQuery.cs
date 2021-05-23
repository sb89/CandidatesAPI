using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Candidates.Queries.Get
{
    public class GetCandidateQuery : IRequest<GetCandidateVm>
    {
        public int Id { get; set; }
    }

    public class GetCandidateQueryHandler : IRequestHandler<GetCandidateQuery, GetCandidateVm>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public GetCandidateQueryHandler(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }
        
        public async Task<GetCandidateVm> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetAsync(request.Id);
            if (candidate == null)
            {
                return null;
            }

            return _mapper.Map<GetCandidateVm>(candidate);
        }
    }
}