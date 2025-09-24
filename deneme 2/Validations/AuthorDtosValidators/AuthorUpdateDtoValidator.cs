using FluentValidation;

namespace deneme_2.Validations.AuthorDtosValidators
{
    public class AuthorUpdateDtoValidator : AbstractValidator<DTOs.AuthorDtos.AuthorUpdateDto>
    {
        public AuthorUpdateDtoValidator() {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");
            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");
            RuleFor(a => a.BirthDate)
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");
        }
    }
}
