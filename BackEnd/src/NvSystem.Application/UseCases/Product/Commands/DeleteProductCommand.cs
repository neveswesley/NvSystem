using MediatR;
using NvSystem.Application.Exceptions;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Product.Commands;

public sealed record DeleteProductCommand (Guid Id) : IRequest<Unit>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Product not found");
        
        _productRepository.DeleteAsync(product);
        await _unitOfWork.Commit();
        
        return Unit.Value;
    }
}