using Microsoft.EntityFrameworkCore;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;
using NvSystem.Infrastructure.Database;

namespace NvSystem.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsWithCategory()
    {
        return await _context.Products.Include(x=>x.Category).ToListAsync();
    }

    public async Task<Product> GetProductByIdWithCategory(Guid id)
    {
        return await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> DisableProduct(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        product.Deactivate();
        return product.Id;
    }

    public async Task<Guid> ActiveProduct(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        product.Activate();
        return product.Id;
    }

    public async Task<Product> GetLookupProduct(string barcode)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Barcode == barcode);
        return product;
    }
}