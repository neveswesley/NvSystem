using Microsoft.EntityFrameworkCore;
using NvSystem.Domain.Entities;

namespace NvSystem.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            
            entity.HasIndex(e=>e.Email)
                .IsUnique();
            
            entity.Property(x => x.CreatedAt)
                .ValueGeneratedNever();
        });
    }
}