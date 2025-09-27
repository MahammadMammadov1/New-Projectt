using FluentValidation;

namespace deneme_2.Validations.AccountDtosValidators
{
    public class LoginDtoValidation : AbstractValidator<DTOs.AccountDtos.LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(x => x.UserName_or_Email)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}
