using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;
using NvSystem.Infrastructure.Database;

namespace NvSystem.Infrastructure.Repositories;

public class SaleItemRepository : BaseRepository<SaleItem>, ISaleItemRepository
{
    private readonly AppDbContext _context;

    public SaleItemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SaleItem> AddSaleItemWithProductId(Guid saleId, Guid productId, int quantity)
    {
        var sale = _context.Sales.FirstOrDefault(x => x.Id == saleId);
        var product = _context.Products.FirstOrDefault(x => x.Id == productId);

        var saleItem = new SaleItem()
        {
            SaleId = sale.Id,
            CreatedAt = DateTime.Now,
            ProductId = productId,
            Id = Guid.NewGuid(),
            IsActive = true,
            ProductName = product.Name ?? string.Empty,
            Quantity = quantity,
            UnitPrice = product.SalePrice
        };
        
        await _context.SaleItems.AddAsync(saleItem);
        return saleItem;
    }
}