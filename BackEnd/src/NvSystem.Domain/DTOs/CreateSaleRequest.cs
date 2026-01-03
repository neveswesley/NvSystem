namespace NvSystem.Domain.DTOs;

public class CreateSaleRequest
{
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
}