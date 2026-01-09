using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface ISaleItemRepository : IBaseRepository<SaleItem>
{
    Task<SaleItem> AddSaleItemWithProductId (Guid saleId, Guid productId, int quantity);
}