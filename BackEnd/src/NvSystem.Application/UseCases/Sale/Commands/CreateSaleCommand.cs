using MediatR;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Domain.Enums;
using NvSystem.Domain.Interfaces;

namespace NvSystem.Application.UseCases.Sale.Commands;

public sealed record CreateSaleCommand : IRequest<Guid>;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
    {
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Domain.Entities.Sale()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            TotalAmount = 0,
            Status = SaleStatus.Pending,
            IsActive = true
        };

        await _saleRepository.CreateAsync(sale);
        await _unitOfWork.Commit();
        
        return sale.Id;
    }
}