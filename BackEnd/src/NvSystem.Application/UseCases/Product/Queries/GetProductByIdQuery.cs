using AutoMapper;
using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Product.Queries;

public sealed record GetProductByIdQuery (Guid Id) : IRequest<ProductResponse>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.GetProductByIdWithCategory(request.Id)
                     ?? throw new NotFoundException("Product not found.");

        var product = new ProductResponse()
        {
            Name = entity.Name,
            Barcode = entity.Barcode,
            SalePrice = entity.SalePrice,
            StockQuantity = entity.StockQuantity,
            CategoryId = entity.CategoryId,
            Category = new CategorySimpleDto()
            {
                Id = entity.CategoryId,
                Name = entity.Category.Name
            },
            IsActive = entity.IsActive
        };
        
        return product;
    }
}