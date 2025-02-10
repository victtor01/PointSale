using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PointSaleApi.Src.Core.Domain;

[Table("storeTables")]
public class StoreTable
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  public required int Number { get; set; }
  public required Guid StoreId { get; set; }
  public required Guid ManagerId { get; set; }

  [JsonIgnore]
  public List<Order> Orders { get; set; } = new List<Order>();
  
  [ForeignKey("StoreId")]
  public Store? Store { get; set; }

  [ForeignKey("ManagerId")]
  public Manager? Manager { get; set; }
}
