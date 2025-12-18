using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NvSystem.Infrastructure.Database
{
    public class AppDbContextFactory 
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=WESLEY\\SQLEXPRESS;Database=NvSystem;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}