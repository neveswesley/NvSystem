using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task DisableCategory(Category category);
    IQueryable<Category> Query();
}