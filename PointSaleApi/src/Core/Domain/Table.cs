using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Domain;

public class Table
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  public required int Number { get; set; }
  public required Manager Manager { get; set; }
}
