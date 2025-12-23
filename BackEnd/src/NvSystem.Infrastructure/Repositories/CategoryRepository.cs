using Microsoft.EntityFrameworkCore;
using NvSystem.Domain.Entities;
using NvSystem.Domain.Interfaces;
using NvSystem.Infrastructure.Database;

namespace NvSystem.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext context, AppDbContext db) : base(context)
    {
        _db = db;
    }

    public async Task DisableCategory(Category category)
    {
        var entity = await _db.Set<Category>().FirstOrDefaultAsync(x=>x.Id == category.Id);
        if (entity != null)
        {
            category.IsActive = false;
            _db.Set<Category>().Update(category);
        }
    }
}