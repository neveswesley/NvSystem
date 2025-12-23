using MediatR;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Query;

public class GetAllCategoryQuery : IRequest<List<Domain.Entities.Category>>
{
    
}

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<Domain.Entities.Category>>
{
    
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Domain.Entities.Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();
        
        return categories;
    }
}