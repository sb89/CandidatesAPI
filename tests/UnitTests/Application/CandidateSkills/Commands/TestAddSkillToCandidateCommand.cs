using System.Threading;
using System.Threading.Tasks;
using Application.CandidateSkills.Commands.Create;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.CandidateSkills.Commands
{
    public class TestAddSkillToCandidateCommand
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICandidateRepository> _candidateRepository;
        private readonly Mock<ISkillRepository> _skillRepository;
        private readonly Mock<ICandidateSkillRepository> _candidateSkillRepository;
        private const int CandidateId = 3;
        private const int SkillId = 2;
        
        public TestAddSkillToCandidateCommand()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            _mapper = config.CreateMapper();
            _candidateRepository = new Mock<ICandidateRepository>();
            _skillRepository = new Mock<ISkillRepository>();
            _candidateSkillRepository = new Mock<ICandidateSkillRepository>();
        }

        [Fact]
        public async Task ShouldFailIfSkillDoesntExist()
        {
            _skillRepository.Setup(x => x.GetAsync(SkillId)).ReturnsAsync((Skill)null);
            
            var handler = new AddSkillToCandidateCommandHandler(_candidateSkillRepository.Object, _skillRepository.Object, 
                _candidateRepository.Object, _mapper);
            var command = new AddSkillToCandidateCommand
            {
                CandidateId = CandidateId,
                SkillId = SkillId
            };
            
            var ex = await Assert.ThrowsAsync<BadRequestException>(async () =>
                await handler.Handle(command, CancellationToken.None));
            
            Assert.Equal("Skill does not exist", ex.Message);
        }
        
        [Fact]
        public async Task ShouldFailIfCandidateDoesntExist()
        {
            _skillRepository.Setup(x => x.GetAsync(SkillId)).ReturnsAsync(new Skill());
            _candidateRepository.Setup(x => x.GetAsync(CandidateId)).ReturnsAsync((Candidate)null);
            
            var handler = new AddSkillToCandidateCommandHandler(_candidateSkillRepository.Object, _skillRepository.Object, 
                _candidateRepository.Object, _mapper);
            var command = new AddSkillToCandidateCommand
            {
                CandidateId = CandidateId,
                SkillId = SkillId
            };
            
            var ex = await Assert.ThrowsAsync<BadRequestException>(async () =>
                await handler.Handle(command, CancellationToken.None));
            
            Assert.Equal("Candidate does not exist", ex.Message);
        }
        
        [Fact]
        public async Task ShouldCreateNewCandidateSKill()
        {
            _skillRepository.Setup(x => x.GetAsync(SkillId)).ReturnsAsync(new Skill());
            _candidateRepository.Setup(x => x.GetAsync(CandidateId)).ReturnsAsync(new Candidate());
            
            var handler = new AddSkillToCandidateCommandHandler(_candidateSkillRepository.Object, _skillRepository.Object, 
                _candidateRepository.Object, _mapper);
            var command = new AddSkillToCandidateCommand
            {
                CandidateId = CandidateId,
                SkillId = SkillId
            };
            
            await handler.Handle(command, CancellationToken.None);
            
            _candidateSkillRepository.Verify(x => x.AddAsync(It.Is<CandidateSkill>(y => y.CandidateId == CandidateId && y.SkillId == SkillId)));
        }
        
        [Fact]
        public async Task ShouldReturnNewId()
        {
            const int id = 123;
            _candidateSkillRepository.Setup(x => x.AddAsync(It.IsAny<CandidateSkill>())).ReturnsAsync(id);
            
            _skillRepository.Setup(x => x.GetAsync(SkillId)).ReturnsAsync(new Skill());
            _candidateRepository.Setup(x => x.GetAsync(CandidateId)).ReturnsAsync(new Candidate());
            
            var handler = new AddSkillToCandidateCommandHandler(_candidateSkillRepository.Object, _skillRepository.Object, 
                _candidateRepository.Object, _mapper);
            var command = new AddSkillToCandidateCommand
            {
                CandidateId = CandidateId,
                SkillId = SkillId
            };
            
            var response = await handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(response, id);
        }
    }
}