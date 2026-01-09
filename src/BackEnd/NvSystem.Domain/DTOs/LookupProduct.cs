namespace NvSystem.Domain.DTOs;

public class LookupProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal SalePrice { get; set; }
    public int StockQuantity { get; set; }
    
}