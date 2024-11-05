using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointSaleApi.src.Core.Domain
{
  public class Store
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required Guid ManagerId { get; set; }

    [ForeignKey("ManagerId")]
    public Manager? Manager { get; set; }
  }
}
