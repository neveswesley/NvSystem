using FluentValidation;
using NvSystem.Communications;
using NvSystem.Communications.Requests;

namespace NvSystem.Application.UseCases.Product;

public class RegisterProductValidator : AbstractValidator<RequestRegisterProductJson>
{
    public RegisterProductValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
            .MinimumLength(2).WithMessage(ResourceMessagesException.NAME_MIN_LENGTH)
            .MaximumLength(100).WithMessage(ResourceMessagesException.NAME_MAX_LENGTH);

        RuleFor(x=>x.SalePrice)
            .NotEmpty().WithMessage(ResourceMessagesException.PRICE_EMPTY)
            .GreaterThan(0).WithMessage(ResourceMessagesException.PRICE_GREATER_THAN);

        RuleFor(x => x.StockQuantity)
            .NotEmpty().WithMessage(ResourceMessagesException.STOCK_QUANTITY_EMPTY)
            .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.STOCK_QUANTITY_GREATER_THAN);
        
        RuleFor(x=>x.CategoryId).NotEmpty().WithMessage(ResourceMessagesException.CATEGORY_ID_EMPTY);
    }
}