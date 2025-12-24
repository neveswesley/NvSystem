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
}