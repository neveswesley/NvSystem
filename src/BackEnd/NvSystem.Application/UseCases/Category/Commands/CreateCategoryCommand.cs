using FluentValidation;
using MediatR;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Category.Commands;

public sealed record CreateCategoryCommand(string Name, string Description) : IRequest<Guid>;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCategoryCommand> _validator;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IValidator<CreateCategoryCommand> validator)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        
        var result = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var category = new Domain.Entities.Category()
        {
            Name = request.Name,
            Description = request.Description
        };

        await _categoryRepository.CreateAsync(category);
        await _unitOfWork.Commit();
        
        return category.Id;
    }
}