using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Domain;

public class StoreTable
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  public required int Number { get; set; }
  public Manager? Manager { get; set; }
  public required Guid ManagerId { get; set; }
}
