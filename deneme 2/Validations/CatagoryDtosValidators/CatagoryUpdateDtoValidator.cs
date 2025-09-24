using deneme_2.DTOs.CatagoryDtos;
using FluentValidation;

namespace deneme_2.Validations.CatagoryDtosValidators
{
    public class CatagoryUpdateDtoValidator : AbstractValidator<CatagoryUpdateDto>
    {
        public CatagoryUpdateDtoValidator() {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Catagory name is required.")
                .MaximumLength(100).WithMessage("Catagory name must not exceed 100 characters.");
            RuleFor(c => c.Description)
                
                .MaximumLength(500).WithMessage("Catagory description must not exceed 500 characters.");
        }
    }
}
