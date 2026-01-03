using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Enums;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.SaleItem.Commands;

public sealed record AddSaleItemCommand(Guid SaleId, Guid ProductId, int Quantity) : IRequest<Unit>;

public class AddSaleItemCommandHandler : IRequestHandler<AddSaleItemCommand, Unit>
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSaleItemCommandHandler(ISaleItemRepository saleItemRepository, IProductRepository productRepository,
        ISaleRepository saleRepository, IUnitOfWork unitOfWork)
    {
        _saleItemRepository = saleItemRepository;
        _productRepository = productRepository;
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddSaleItemCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId) ??
                      throw new NotFoundException("Product not found.");

        var sale = await _saleRepository.GetByIdAsync(request.SaleId) ??
                   throw new NotFoundException("Sale not found.");

        if (sale.Status != SaleStatus.Pending) throw new BadRequestException("A finalized sale cannot be modified.");

        if (product.StockQuantity < request.Quantity) throw new BadRequestException("Insufficient stock available.");
        
        if (request.Quantity < 0) throw new BadRequestException("Quantity cannot be negative.");

        var saleItem = new Domain.Entities.SaleItem()
        {
            SaleId = request.SaleId,
            ProductId = request.ProductId,
            ProductName = product.Name,
            Quantity = request.Quantity,
            UnitPrice = product.SalePrice
        };

        sale.TotalAmount += saleItem.Total;
        product.DecreaseStock(request.Quantity);

        await _saleItemRepository.CreateAsync(saleItem);
        await _unitOfWork.Commit();

        return Unit.Value;
    }
}