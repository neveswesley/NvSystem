using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Enums;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.SaleItem.Queries;

public sealed record GetSaleByIdQuery(Guid Id) : IRequest<SaleResponse>;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleResponse>
{
    private readonly ISaleRepository _repository;

    public GetSaleByIdQueryHandler(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<SaleResponse> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _repository.GetSaleByIdWithItemsAsync(request.Id, cancellationToken);

        var response = new SaleResponse()
        {
            Id = sale.Id,
            SaleDate = sale.CreatedAt,
            TotalAmount = sale.TotalAmount,
            Status = sale.Status.ToString(),
            Items = sale.Items.Select(i => new SaleItemResponse()
            {
                Id = i.Id,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Total = i.Total
            }).ToList()
        };
        
        return response;
    }
}