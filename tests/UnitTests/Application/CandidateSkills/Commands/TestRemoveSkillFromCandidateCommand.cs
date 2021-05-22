using System.Threading;
using System.Threading.Tasks;
using Application.CandidateSkills.Commands.Delete;
using Application.Common.Interfaces.Repositories;
using Moq;
using Xunit;

namespace UnitTests.Application.CandidateSkills.Commands
{
    public class TestRemoveSkillFromCandidateCommand
    {
        private readonly Mock<ICandidateSkillRepository> _repository;
        
        public TestRemoveSkillFromCandidateCommand()
        {
            _repository = new Mock<ICandidateSkillRepository>();
        }

        [Fact]
        public async Task ShouldRemoveSkillFromCandidate()
        {
            const int candidateId = 2;
            const int skillId = 3;
            
            var command = new RemoveSkillFromCandidateCommand
            {
                CandidateId = candidateId,
                SkillId = skillId
            };

            var handle = new RemoveSkillFromCandidateCommandHandler(_repository.Object);

            await handle.Handle(command, CancellationToken.None);
            
            _repository.Verify(x => x.DeleteAsync(candidateId, skillId));
        }
    }
}