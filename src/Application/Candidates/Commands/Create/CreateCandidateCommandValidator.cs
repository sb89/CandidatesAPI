using FluentValidation;

namespace Application.Candidates.Commands.Create
{
    public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateCommandValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Surname).MaximumLength(50).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Address1).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Town).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Country).MaximumLength(50).NotEmpty();
            RuleFor(x => x.PostCode).MaximumLength(20).NotEmpty();
            RuleFor(x => x.PhoneHome).MaximumLength(50).NotEmpty();
            RuleFor(x => x.PhoneMobile).MaximumLength(50).NotEmpty();
            RuleFor(x => x.PhoneWork).MaximumLength(50).NotEmpty();
        }
    }
}