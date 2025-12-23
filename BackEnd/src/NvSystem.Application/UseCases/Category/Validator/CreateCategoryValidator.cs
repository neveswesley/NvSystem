using FluentValidation;
using NvSystem.Application.UseCases.Category.Commands;

namespace NvSystem.Application.UseCases.Category.Validator;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");
        
        RuleFor(category => category.Description).
            NotEmpty().WithMessage("Description cannot be empty.")
            .Length(2, 250).WithMessage("Description must be between 2 and 250 characters.");
    }
}