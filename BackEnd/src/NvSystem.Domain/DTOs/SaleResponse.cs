using NvSystem.Domain.Entities;
using NvSystem.Domain.Enums;

namespace NvSystem.Domain.DTOs;

public class SaleResponse
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleItemResponse> Items { get; set; }
}