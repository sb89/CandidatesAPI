using FluentValidation;

namespace Application.CandidateSkills.Commands.Delete
{
    public class RemoveSkillFromCandidateCommandValidator : AbstractValidator<RemoveSkillFromCandidateCommand>
    {
        public RemoveSkillFromCandidateCommandValidator()
        {
            RuleFor(x => x.SkillId).NotEmpty();
            RuleFor(x => x.CandidateId).NotEmpty();
        }
    }
}