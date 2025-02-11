using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Database;

internal static class Extensions
{
  public static void ProductsConfigure(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Product>(productEntity =>
    {
      productEntity.Property(p => p.Id).HasColumnName("uuid").IsRequired();
      productEntity.HasKey(p => p.Id);
      productEntity.HasOne(p => p.Store).WithMany(s => s.Products).HasForeignKey(p => p.StoreId);
      productEntity.HasOne(p => p.Store).WithMany().HasForeignKey(p => p.StoreId);

      productEntity
        .HasMany(p => p.Options)
        .WithOne(o => o.Product)
        .HasForeignKey(o => o.ProductId);
    });
  }

  public static void ManagersConfigure(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Manager>(managerEntity =>
    {
      managerEntity.Property(m => m.Id).HasColumnType("uuid").IsRequired();
      managerEntity.HasKey(m => m.Id);
      managerEntity.HasIndex(m => m.Email).IsUnique();
      managerEntity
        .HasMany(m => m.Stores)
        .WithOne(s => s.Manager)
        .HasForeignKey(s => s.ManagerId);
    });
  }

  public static void StoresConfigure(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Store>(storeEntity =>
    {
      storeEntity.Property(e => e.Id).HasColumnType("uuid").IsRequired();
      storeEntity.HasKey(e => e.Id);
      storeEntity.HasMany(s => s.Products);
      storeEntity.HasMany(s => s.Tables);
      storeEntity
        .HasOne(e => e.Manager)
        .WithMany(manager => manager.Stores)
        .HasForeignKey(store => store.ManagerId);
    });
  }

  public static void TablesConfigure(this ModelBuilder builder)
  {
    builder.Entity<StoreTable>(table =>
    {
      table.Property(e => e.Id).HasColumnType("uuid");
      table.HasKey(e => e.Id);
      table.HasOne(e => e.Store).WithMany(store => store.Tables).HasForeignKey(e => e.StoreId);
    });
  }

  public static void OrdersConfigure(this ModelBuilder builder)
  {
    builder.Entity<Order>(table =>
    {
      table.Property(e => e.Id).HasColumnType("uuid");
      table.HasKey(e => e.Id);
      table.HasMany(e => e.OrderProducts).WithOne(o => o.Order).HasForeignKey(o => o.OrderId);
    });
  }

  public static void OrderProductsConfigure(this ModelBuilder builder)
  {
    builder.Entity<OrderProduct>(table =>
    {
      table.HasKey(e => e.Id);
      table.HasOne(e => e.Order).WithMany(o => o.OrderProducts).HasForeignKey(e => e.OrderId);
    });
  }
}

public class DatabaseContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
  public DbSet<Product> Products { get; set; }
    
  public DbSet<OptionsProduct> OptionsProducts { get; set; } = null!;

  public DbSet<Order> Orders { get; set; } = null!;
    
  public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
  public DbSet<Manager> Managers { get; set; } = null!;
  public DbSet<Store> Stores { get; set; } = null!;
  public DbSet<StoreTable> Tables { get; set; } = null!;

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    foreach (var entry in ChangeTracker.Entries())
    {
      if (entry.State == EntityState.Modified) 
      {
        if (entry.Entity is BaseEntity entity)
        {
          entity.UpdatedAt = DateTime.UtcNow;
        }
      }
    }
    
    return await base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ManagersConfigure();
    modelBuilder.StoresConfigure();
    modelBuilder.TablesConfigure();
    modelBuilder.OrdersConfigure();
    modelBuilder.OrderProductsConfigure();
  }
}