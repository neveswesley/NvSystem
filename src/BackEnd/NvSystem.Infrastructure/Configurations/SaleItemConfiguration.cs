using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvSystem.Domain.Entities;

namespace NvSystem.Infrastructure.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItem");
        
        builder.HasKey(c => c.Id);
        
        builder.HasOne(s=>s.Sale).WithMany(s=>s.Items).HasForeignKey(s=>s.SaleId);
    }
}