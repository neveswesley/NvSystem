using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync (T entity);
    void UpdateAsync (T entity);
    void DeleteAsync (T entity);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllActiveAsync();
    Task<T?> GetByIdAsync (Guid id);
}