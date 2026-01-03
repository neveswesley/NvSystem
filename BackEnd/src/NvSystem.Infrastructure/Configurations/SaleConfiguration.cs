using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NvSystem.Domain.Entities;

namespace NvSystem.Infrastructure.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        
        builder.HasKey(c => c.Id);
        
        builder.HasMany(s=>s.Items).WithOne(c=>c.Sale).HasForeignKey(c=>c.SaleId);
    }
}