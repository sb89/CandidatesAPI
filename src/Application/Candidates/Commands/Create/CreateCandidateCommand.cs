using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Candidates.Commands.Create
{
    public class CreateCandidateCommand : IRequest<int>
    {
        public string FirstName { get; set; }

        public long DateOfBirth { get; set; }

        public string Surname { get; set; }

        public string Address1 { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string PhoneHome { get; set; }

        public string PhoneMobile { get; set; }

        public string PhoneWork { get; set; }
    }

    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, int>
    {
        private readonly ICandidateRepository _repository;
        private readonly IMapper _mapper;

        public CreateCandidateCommandHandler(ICandidateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = _mapper.Map<Candidate>(request);

            return await _repository.CreateAsync(candidate);
        }
    }
}