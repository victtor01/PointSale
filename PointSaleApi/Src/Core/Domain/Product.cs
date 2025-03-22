using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointSaleApi.Src.Core.Domain;

[Table("products")]
public class Product
{
  [Key]
  public Guid Id { get; init; } = Guid.NewGuid();

  [MinLength(3)]
  [MaxLength(50)]
  public required string Name { get; set; }

  [Range(0.01, float.MaxValue, ErrorMessage = "Price must be greater than 0.")]
  public required float Price { get; set; }

  [ForeignKey("StoreId")]
  public Store? Store { get; set; }

  [Column("storeId")]
  public Guid StoreId { get; set; }

  [Column("quantity")]
  public int? Quantity { get; set; } = null;

  [Column("description")]
  [MaxLength(500)]
  public string? Description { get; set; } = null;

  public List<OptionsProduct> Options { get; set; } = [];
}