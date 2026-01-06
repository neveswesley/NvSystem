using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;


namespace NvSystem.Application.UseCases.Sale.Queries;

public class GetSalesQuery : IRequest<PagedResult<SaleResponse>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, PagedResult<SaleResponse>>
{
    
    private readonly ISaleRepository _repository;

    public GetSalesQueryHandler(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<SaleResponse>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.Query().OrderByDescending(x => x.CreatedAt);
        
        var totalItems = query.Count();

        var items =  query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new SaleResponse
            {
                Id = x.Id,
                SaleDate = x.CreatedAt,
                Status = x.Status.ToString(),
                TotalAmount = x.TotalAmount,
                Items = x.Items.Select(x=> new SaleItemResponse
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Total = x.Total
                }).ToList()
            }).ToList();

        return new PagedResult<SaleResponse>()
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            Items = items,
        };

    }
}