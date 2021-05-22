using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Application.Common.Mappings;
using Application.Skills.Queries.GetAll;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.Skills.Queries
{
    public class TestGetAllQuery
    {
        private readonly IMapper _mapper;
        private readonly Mock<ISkillRepository> _repository;

        public TestGetAllQuery()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            _mapper = config.CreateMapper();
            _repository = new Mock<ISkillRepository>();
        }
        
        [Fact]
        public async Task ShouldReturnAllSkills()
        {
            var skills = new List<Skill>
            {
                new(), new(), new()
            };

            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(skills);
            
            var handler = new GetAllSkillsQueryHandler(_mapper, _repository.Object);

            var response = await handler.Handle(new GetAllSkillsQuery(), CancellationToken.None);
            
            Assert.Equal(skills.Count, response.Skills.Count());
        }
    }
}