namespace NvSystem.Communications.Requests;

public class RequestRegisterProductJson
{
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal SalePrice { get; set; } = decimal.Zero;
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
}