using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointSaleApi.Src.Core.Application.Enums;

namespace PointSaleApi.Src.Core.Domain;

[Table("orders")]
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

  [Column("status")] public OrderStatus Status { get; set; } = OrderStatus.CURRENT;
  
  [Required]
  [Column("storeId")]
  public Guid StoreId { get; set; }
  
  [Column("order_products")]
  public List<OrderProduct?> OrderProducts { get; set; } = [];
  
  [ForeignKey(nameof(ManagerId))]
  public Manager? manager { get; set; }
  
  [ForeignKey(nameof(TableId))]
  public StoreTable? Table { get; set; }
  
  [ForeignKey(nameof(StoreId))]
  public Store? Store { get; set; }
}