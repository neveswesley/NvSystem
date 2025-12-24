using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;

namespace NvSystem.Domain.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> GetAllProductsWithCategory();
}