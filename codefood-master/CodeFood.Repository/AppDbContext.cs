using CodeFood.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeFood.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new UserMap(modelBuilder.Entity<User>());
        modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 4)");
    }
}
