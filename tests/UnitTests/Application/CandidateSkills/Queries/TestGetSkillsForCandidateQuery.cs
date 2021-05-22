using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CandidateSkills.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Mappings;
using Application.Skills.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.CandidateSkills.Queries
{
    public class TestGetSkillsForCandidateQuery
    {
        private readonly IMapper _mapper;
        private readonly Mock<ISkillRepository> _repository;

        public TestGetSkillsForCandidateQuery()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            _mapper = config.CreateMapper();
            _repository = new Mock<ISkillRepository>();
        }
        
        [Fact]
        public async Task ShouldReturnCandidateSkills()
        {
            const int candidateId = 5;
            var skills = new List<Skill>
            {
                new(), new(), new()
            };

            _repository.Setup(x => x.GetSkillsForCandidateAsync(candidateId)).ReturnsAsync(skills);
            
            var handler = new GetSkillsForCandidateQueryHandler( _repository.Object, _mapper);
            var query = new GetSkillsForCandidateQuery
            {
                CandidateId = candidateId
            };
            
            var response = await handler.Handle(query, CancellationToken.None);
            
            Assert.Equal(skills.Count, response.Skills.Count());
        }
    }
}