using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
  order => varios produtos respectivamente com seus ExtraProduct
 */

/*
  order [
    {
      quantity: 4
      productId: "PRODUCTID"
      extras: [
        { EXTRA }
        { EXTRA }
        { EXTRA }
      ]
    }
    {
      quantity: 2
      productId: "PRODUCTID"
      extras: [
        { EXTRA }
        { EXTRA }
        { EXTRA }
      ]
    }
  ]
 */

namespace PointSaleApi.Src.Core.Domain
{
  public class Product
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MinLength(3)]
    public required string Name { get; set; }

    [Range(0.01, float.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public required float Price { get; set; }
    public required Guid ManagerId { get; set; }
    [ForeignKey("ManagerId")]
    public Manager? Manager { get; set; }
    [ForeignKey("StoreId")]
    public Store? Store { get; set; }
    public Guid StoreId { get; set; }
    public int? Quantity { get; set; } = null;
    public string? Description { get; set; } = null;
    public OptionsProduct[] Options { get; set; } = [];
  }

  public class OptionsProduct
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MinLength(3)]
    public required string Name { get; set; }

    [Range(0.01, float.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public float Price { get; set; }

    [ForeignKey("productId")]
    public Product? Product { get; set; }

    [Required]
    public Guid? ProductId { get; set; }
  }
}
