using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Product.Queries;

public sealed record GetLookupByBarcode(string Barcode) : IRequest<LookupProduct>;

public class GetLookUpByBarcode : IRequestHandler<GetLookupByBarcode, LookupProduct>
{
    private readonly IProductRepository _productRepository;

    public GetLookUpByBarcode(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<LookupProduct> Handle(GetLookupByBarcode request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.GetLookupProduct(request.Barcode) ?? throw new NotFoundException("Product not found");
        
        var product = new LookupProduct()
        {
            Id = entity.Id,
            Name = entity.Name,
            SalePrice = entity.SalePrice,
            StockQuantity = entity.StockQuantity
        };
        
        return product;
    }
}