using System.Threading;
using System.Threading.Tasks;
using Application.Candidates.Commands.Update;
using Application.Common.Exceptions;
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
        
        [Fact]
        public async Task ShouldFailIfUpdateDoesNotUpdateExpectedNumberOfRows()
        {
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Candidate>())).ReturnsAsync(0);
            
            var handler = new UpdateCommandHandler(_mapper, _repository.Object);

            var ex = await Assert.ThrowsAsync<BadRequestException>(async () =>
                await handler.Handle(new UpdateCommand(), CancellationToken.None));
            
            Assert.Equal("Did not update the expected number of rows", ex.Message);
        }
    }
}