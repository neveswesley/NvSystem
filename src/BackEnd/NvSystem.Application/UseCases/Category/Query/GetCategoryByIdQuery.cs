using AutoMapper;
using MediatR;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Query;

public sealed record GetCategoryByIdQuery (Guid Id) : IRequest<GetCategoryResponse>;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryResponse>
{
    
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<GetCategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        var result = _mapper.Map<GetCategoryResponse>(category);
        return result;
    }
}