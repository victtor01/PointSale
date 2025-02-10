
using System.Text.Json.Serialization;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class TableDTO
{
  public required int Number { get; set; }
  public required Guid Id { get; set; }
  public List<OrderDTO?> Orders { get; set; } = new List<OrderDTO?>();
}