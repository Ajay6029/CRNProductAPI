using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Item> Items => Set<Item>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.ProductName)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(p => p.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.CreatedOn)
                .IsRequired();

            // Index for better query performance
            entity.HasIndex(p => p.CreatedOn)
                .HasDatabaseName("IX_Product_CreatedOn");

            entity.Property(p => p.ModifiedBy)
                .HasMaxLength(100);

            entity.HasMany(p => p.Items)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Quantity)
                .IsRequired();
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Token)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasIndex(r => r.Token)
                .IsUnique();

            entity.Property(r => r.Username)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(r => r.ExpiresOn)
                .IsRequired();

            entity.Property(r => r.CreatedOn)
                .IsRequired();
        });
    }
}