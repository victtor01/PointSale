using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointSaleApi.Src.Core.Domain;

[Table("products_options")]
public class OptionsProduct
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();

  [MinLength(3)]
  public required string Name { get; set; }
    
  [Range(0.01, float.MaxValue, ErrorMessage = "Price must be greater than 0.")]
  public float Price { get; set; }

  [Required]
  public Guid? ProductId { get; set; }

  [ForeignKey(nameof(ProductId))]
  public Product? Product { get; set; }
}