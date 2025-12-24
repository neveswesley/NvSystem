using NvSystem.Domain.Entities;

namespace NvSystem.Domain.DTOs;

public class ProductResponse
{
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal SalePrice { get; set; } = decimal.Zero;
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
    public CategorySimpleDto Category { get; set; }
    public bool IsActive { get; set; }
}

public class CategorySimpleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}