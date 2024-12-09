using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Database
{
  public class DatabaseContext(DbContextOptions contextOptions) : DbContext(contextOptions)
  {
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      ManagersConfigure(modelBuilder);
      StoresConfigure(modelBuilder);
    }

    private static void ManagersConfigure(ModelBuilder modelBuilder)
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

    private static void StoresConfigure(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Store>(storeEntity =>
      {
        storeEntity.Property(e => e.Id).HasColumnType("uuid");
        storeEntity.HasKey(e => e.Id);
        storeEntity
          .HasOne(e => e.Manager)
          .WithMany(manager => manager.Stores)
          .HasForeignKey(store => store.ManagerId);
      });
    }
  }
}
