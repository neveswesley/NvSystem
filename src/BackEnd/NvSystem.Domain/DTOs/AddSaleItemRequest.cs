namespace NvSystem.Domain.DTOs;

public class AddSaleItemRequest
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}