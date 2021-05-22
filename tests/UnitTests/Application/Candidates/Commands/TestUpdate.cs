using System.Threading;
using System.Threading.Tasks;
using Application.Candidates.Commands.Update;
using Application.Common.Interfaces.Repositories;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.Candidates.Commands
{
    public class TestUpdate
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICandidateRepository> _repository;
        
        public TestUpdate()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            _mapper = config.CreateMapper();
            _repository = new Mock<ICandidateRepository>();
        }
        
        [Fact]
        public async Task ShouldUpdateCandidate()
        {
            var handler = new UpdateCommandHandler(_mapper, _repository.Object);

            await handler.Handle(new UpdateCommand(), CancellationToken.None);
            
            _repository.Verify(x => x.UpdateAsync(It.IsAny<Candidate>()));
        }
    }
}