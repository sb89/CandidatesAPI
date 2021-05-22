using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Candidates.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public int DateOfBirth { get; set; }

        public string Surname { get; set; }

        public string Address1 { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string PhoneHome { get; set; }

        public string PhoneMobile { get; set; }

        public string PhoneWork { get; set; }
        
    }
    
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICandidateRepository _repository;

        public UpdateCommandHandler(IMapper mapper, ICandidateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var candidate = _mapper.Map<Candidate>(request);

            var rowsModified = await _repository.UpdateAsync(candidate);
            if (rowsModified != 1)
            {
                throw new BadRequestException("Did not update the expected number of rows");
            }

            return Unit.Value;
        }
    }
}