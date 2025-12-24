using AutoMapper;
using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Product.Queries;

public sealed record GetAllProductsQuery : IRequest<List<ProductResponse>>
{
    
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
{
    
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsWithCategory();
        return _mapper.Map<List<ProductResponse>>(products);
    }
}