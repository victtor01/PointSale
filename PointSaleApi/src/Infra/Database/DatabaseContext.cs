using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Database
{
  internal static class Extensions
  {
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
  }

  public class DatabaseContext(DbContextOptions contextOptions) : DbContext(contextOptions)
  {
    public DbSet<Manager> Managers { get; set; } = null!;
    public DbSet<Store> Stores { get; set; } = null!;
    public DbSet<StoreTable> Tables { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ManagersConfigure();
      modelBuilder.StoresConfigure();
      modelBuilder.TablesConfigure();
    }
  }
}
