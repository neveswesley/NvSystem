using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Commands;

public sealed record DisableCategoryCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DisableCategoryCommand(Guid id)
    {
        Id = id;
    }
}

public class DisableCategoryCommandHandler : IRequestHandler<DisableCategoryCommand, Unit>
{
    
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DisableCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DisableCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        
        if (category == null)
            throw new NotFoundException("Category not found.");
        
        await _categoryRepository.DisableCategory(category);
        
        await _unitOfWork.Commit();
        
        return Unit.Value;

    }
}