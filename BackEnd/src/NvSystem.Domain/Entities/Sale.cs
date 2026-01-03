using NvSystem.Domain.Enums;

namespace NvSystem.Domain.Entities;

public class Sale : BaseEntity
{
    public decimal TotalAmount { get; set; }
    public SaleStatus Status { get; set; }
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}