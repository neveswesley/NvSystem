using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> GetAllProductsWithCategory();
    Task<Product> GetProductByIdWithCategory(Guid id);
    Task<Guid> DisableProduct(Guid id);
}