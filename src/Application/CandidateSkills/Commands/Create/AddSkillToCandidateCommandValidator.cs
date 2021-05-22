using FluentValidation;

namespace Application.CandidateSkills.Commands.Create
{
    public class AddSkillToCandidateCommandValidator : AbstractValidator<AddSkillToCandidateCommand>
    {
        public AddSkillToCandidateCommandValidator()
        {
            RuleFor(x => x.CandidateId).NotEmpty();
            RuleFor(x => x.SkillId).NotEmpty();
        }
    }
}