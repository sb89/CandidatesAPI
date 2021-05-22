using System.Threading;
using System.Threading.Tasks;
using Application.Candidates.Commands.Create;
using Application.Common.Interfaces.Repositories;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.Candidates.Commands
{
    public class TestCreate
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICandidateRepository> _repository;
        
        public TestCreate()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            _mapper = config.CreateMapper();
            _repository = new Mock<ICandidateRepository>();
        }

        [Fact]
        public async Task ShouldCreateCandidate()
        {
            var handler = new CreateCommandHandler(_repository.Object, _mapper);

            await handler.Handle(new CreateCommand(), CancellationToken.None);
            
            _repository.Verify(x => x.CreateAsync(It.IsAny<Candidate>()));
        }
        
        [Fact]
        public async Task ShouldReturnNewId()
        {
            const int id = 123;
            _repository.Setup(x => x.CreateAsync(It.IsAny<Candidate>())).ReturnsAsync(id);
            
            var handler = new CreateCommandHandler(_repository.Object, _mapper);

            var response = await handler.Handle(new CreateCommand(), CancellationToken.None);
            
            Assert.Equal(response, id);
        }
        
    }
}