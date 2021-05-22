using FluentValidation;

namespace Application.CandidateSkills.Commands.Create
{
    public class AddSkillToCandidateCommandValidator : AbstractValidator<AddSkillToCandidateCommand>
    {
        public AddSkillToCandidateCommandValidator()
        {
            RuleFor(x => x.CandidateId).NotNull();
            RuleFor(x => x.SkillId).NotNull();
        }
    }
}