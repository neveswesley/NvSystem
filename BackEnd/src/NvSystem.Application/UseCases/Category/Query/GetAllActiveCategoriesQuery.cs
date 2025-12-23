using AutoMapper;
using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Query;

public sealed record GetAllActiveCategoriesQuery : IRequest<List<GetCategoryResponse>>;

public class GetAllActiveCategoryQueryHandler : IRequestHandler<GetAllActiveCategoriesQuery, List<GetCategoryResponse>>
{
    
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllActiveCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetCategoryResponse>> Handle(GetAllActiveCategoriesQuery request, CancellationToken cancellationToken)
    {
        var activeCategories = await _categoryRepository.GetAllActiveAsync();
        var result = _mapper.Map<List<GetCategoryResponse>>(activeCategories);
        return result;
    }
}