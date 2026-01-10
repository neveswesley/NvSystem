namespace NvSystem.Communications.Responses;

public class ResponseRegisterProductJson
{
    public string Name { get; set; }
    public string Barcode { get; set; }
    public decimal SalePrice { get; set; }
    public int StockQuantity { get; set; }
    public Guid CategoryId { get; set; }
}