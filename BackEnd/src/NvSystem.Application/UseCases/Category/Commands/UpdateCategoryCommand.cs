using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Commands;

public sealed record UpdateCategoryCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public UpdateCategoryDto Dto { get; set; }

    public UpdateCategoryCommand(Guid id, UpdateCategoryDto dto)
    {
        Id = id;
        Dto = dto;
    }
}

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new NotFoundException("Category not found.");
        
        category.Name = string.IsNullOrWhiteSpace(request.Dto.Name) ? category.Name : request.Dto.Name;
        category.Description = string.IsNullOrWhiteSpace(request.Dto.Description) ? category.Description : request.Dto.Description;

        await _unitOfWork.Commit();
        
        return Unit.Value;

    }
}