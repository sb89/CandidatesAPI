using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Candidates.Queries.GetAll;
using Application.Common.Interfaces.Repositories;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.Candidates.Queries
{
    public class TestGetAll
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICandidateRepository> _repository;

        public TestGetAll()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            _mapper = config.CreateMapper();
            _repository = new Mock<ICandidateRepository>();
        }
        
        [Fact]
        public async Task ShouldReturnAllCandidates()
        {
            var candidates = new List<Candidate>
            {
                new(), new(), new()
            };

            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(candidates);
            
            var handler = new GetAllCandidatesQueryHandler(_repository.Object, _mapper);

            var response = await handler.Handle(new GetAllCandidatesQuery(), CancellationToken.None);
            
            Assert.Equal(candidates.Count, response.Candidates.Count());
        }
    }
}