using MediatR;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Product.Commands;

public sealed record DisableProductCommand(Guid Id) : IRequest<Unit>;

public class DisableProductCommandHandler : IRequestHandler<DisableProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DisableProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DisableProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DisableProduct(request.Id);
        await _unitOfWork.Commit();
        return Unit.Value;
    }
}