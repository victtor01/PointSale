using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointSaleApi.Src.Core.Domain;

[Table("order_products")]
public class OrderProduct
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [Column("orderId")]
  public required Guid OrderId { get; set; }
  
  [Column("productId")]
  public required Guid ProductId { get; set; }
  
  [ForeignKey(nameof(OrderId))]
  public Order? Order { get; set; }
  
  [ForeignKey(nameof(ProductId))]
  public Product? Product { get; set; }  
}