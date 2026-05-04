using Microsoft.EntityFrameworkCore;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items => Set<Item>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Items");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Description)
                .HasMaxLength(500);

            entity.Property(x => x.Stock)
                .IsRequired();
        });
    }
}
