using AutoMapper;
using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Query;

public class GetAllCategoryQuery : IRequest<List<GetCategoryResponse>>
{
    
}

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<GetCategoryResponse>>
{
    
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetCategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();

        var result = _mapper.Map<List<GetCategoryResponse>>(categories);
        
        return result;
    }
}