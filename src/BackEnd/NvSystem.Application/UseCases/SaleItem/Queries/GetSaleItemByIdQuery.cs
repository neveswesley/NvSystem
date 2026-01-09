using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.SaleItem.Queries;

public sealed record GetSaleItemByIdQuery(Guid Id) : IRequest<SaleItemResponse>;

public class GetSaleByIdHandler : IRequestHandler<GetSaleItemByIdQuery, SaleItemResponse>
{
    private readonly ISaleItemRepository _repository;

    public GetSaleByIdHandler(ISaleItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<SaleItemResponse> Handle(GetSaleItemByIdQuery request, CancellationToken cancellationToken)
    {
        var saleItem = await _repository.GetByIdAsync(request.Id);

        var result = new SaleItemResponse()
        {
            Id = saleItem.Id,
            ProductName = saleItem.ProductName,
            Quantity = saleItem.Quantity,
            UnitPrice = saleItem.UnitPrice,
            Total = saleItem.Total
        };

        return result;
    }
}