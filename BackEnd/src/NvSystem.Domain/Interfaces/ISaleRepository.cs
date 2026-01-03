using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<Sale> GetSaleByIdWithItemsAsync(Guid id, CancellationToken cancellationToken);
}