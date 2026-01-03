using Microsoft.EntityFrameworkCore;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;
using NvSystem.Infrastructure.Database;

namespace NvSystem.Infrastructure.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Sale> GetSaleByIdWithItemsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Sales.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}