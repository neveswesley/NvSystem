using FluentValidation;
using NvSystem.Application.UseCases.Product.Commands;

namespace NvSystem.Application.UseCases.Product.Validator;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
            .MaximumLength(100).WithMessage("Name must be no more than 100 characters");

        RuleFor(x=>x.SalePrice)
            .NotEmpty().WithMessage("Sale price cannot be empty")
            .GreaterThan(0).WithMessage("Sale price must be greater than 0");

        RuleFor(x => x.StockQuantity)
            .NotEmpty().WithMessage("Stock quantity cannot be empty")
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than 0");
        
        RuleFor(x=>x.CategoryId).NotEmpty().WithMessage("Category id cannot be empty");

    }
}