using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain;

[Table("order_products")]
public class OrderProduct : BaseEntity
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();

  [Column("quantity")]
  public required int Quantity { get; set; }

  [Column("options")]
  public List<OptionsProduct> OptionsProducts { get; set; } = new List<OptionsProduct>();

  [Required] 
  [Column("status")]
  public OrderProductStatus Status { get; set; } = OrderProductStatus.PENDING;
  
  [Required]
  [Column("storeId")]
  public required Guid StoreId { get; set; }

  [Column("orderId")]
  public required Guid OrderId { get; set; }

  [Column("productId")]
  public required Guid ProductId { get; set; }

  [Required]
  [ForeignKey(nameof(StoreId))]
  public Store? Store { get; set; }

  [ForeignKey(nameof(OrderId))]
  public Order? Order { get; set; }
  
  [ForeignKey(nameof(ProductId))]
  public Product? Product { get; set; }

  public void IsValidStoreId(Guid storeId)
  {
    if (storeId == Guid.Empty || storeId != this.StoreId)
      throw new BadRequestException("Invalid storeId");
  }

  public void setStatusWithString(string status)
  {
    OrderProductStatus newStatus = Enum.TryParse<OrderProductStatus>(status, out var STATUS)
      ? STATUS
      : throw new BadRequestException("Status is not valid");

    this.Status = newStatus;

  }
}