using deneme_2.DTOs.BookDtos;
using FluentValidation;

namespace deneme_2.Validations.BookDtosValidators
{
    public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateDtoValidator()
        {
           
            RuleFor(b => b.Title)
                
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
            RuleFor(b => b.Description)
                
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
            RuleFor(b => b.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(b => b.ReleaseDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Release date cannot be in the future.");
            RuleFor(b => b.AuthorId)
                .GreaterThan(0).WithMessage("AuthorId must be a positive integer.");
            RuleFor(b => b.CatagoryId)
                .GreaterThan(0).WithMessage("CatagoryId must be a positive integer.");

        }
    }
}
