using AutoMapper;
using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Query;

public class GetAllCategoryQuery : IRequest<PagedResult<GetCategoryResponse>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, PagedResult<GetCategoryResponse>>
{
    
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetCategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var query = _categoryRepository.Query().OrderByDescending(x=>x.CreatedAt);
        
        var totalItems = query.Count();
        
        var items = query.Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x=> new GetCategoryResponse()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CreatedAt = x.CreatedAt,
            IsActive = x.IsActive
        }).ToList();
        
        return new PagedResult<GetCategoryResponse>()
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            Items = items,
        };
    }
}