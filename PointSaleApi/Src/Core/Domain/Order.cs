using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PointSaleApi.Src.Core.Domain;

public class Order
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [Required]
  [Column("tableId")]
  public Guid TableId { get; set; }
  
  [Required]
  [Column("managerId")]
  public Guid ManagerId { get; set; }
  
  [Required]
  [Column("storeId")]
  public Guid StoreId { get; set; }
  
  [ForeignKey(nameof(ManagerId))]
  public Manager? manager { get; set; }
  
  [Column("order_products")]
  public List<OrderProduct?> OrderProducts { get; set; } = [];
  
  [ForeignKey(nameof(TableId))]
  public StoreTable? Table { get; set; }
  
  [ForeignKey(nameof(StoreId))]
  public Store? Store { get; set; }
}