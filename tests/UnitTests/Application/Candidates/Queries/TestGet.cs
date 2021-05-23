using System.Threading;
using System.Threading.Tasks;
using Application.Candidates.Queries.Get;
using Application.Common.Interfaces.Repositories;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.Candidates.Queries
{
    public class TestGet
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICandidateRepository> _repository;

        public TestGet()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            _mapper = config.CreateMapper();
            _repository = new Mock<ICandidateRepository>();
        }
        
        [Fact]
        public async Task ShouldReturnNullIfNotFound()
        {
            const int candidateId = 5;
            _repository.Setup(x => x.GetAsync(candidateId)).ReturnsAsync((Candidate)null);
            
            var handler = new GetCandidateQueryHandler(_repository.Object, _mapper);
            var query = new GetCandidateQuery
            {
                Id = candidateId
            };

            var response = await handler.Handle(query, CancellationToken.None);
            
            Assert.Null(response);
        }
        
        [Fact]
        public async Task ShouldReturnVmIfFound()
        {
            const int candidateId = 5;
            _repository.Setup(x => x.GetAsync(candidateId)).ReturnsAsync(new Candidate());
            
            var handler = new GetCandidateQueryHandler(_repository.Object, _mapper);
            var query = new GetCandidateQuery
            {
                Id = candidateId
            };

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.IsType<GetCandidateVm>(response);
        }
    }
}