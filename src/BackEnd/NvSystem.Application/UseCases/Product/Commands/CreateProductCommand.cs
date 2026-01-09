using FluentValidation;
using MediatR;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Product.Commands;

public sealed record CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public decimal SalePrice { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }

    public CreateProductCommand(string name, decimal salePrice, int stockQuantity, Guid categoryId)
    {
        Name = name;
        SalePrice = salePrice;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IBarcodeGenerator _barcodeGenerator;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork,
        IValidator<CreateProductCommand> validator, IBarcodeGenerator barcodeGenerator)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _barcodeGenerator = barcodeGenerator;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var barcode = _barcodeGenerator.Generate();

        var product = new Domain.Entities.Product(request.Name, barcode, request.SalePrice, request.StockQuantity,
            request.CategoryId);

        await _productRepository.CreateAsync(product);
        await _unitOfWork.Commit();

        return product.Id;
    }
}