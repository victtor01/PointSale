using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class EmployeePositionDTO
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public List<string> Permissions { get; set; } = [];
  public List<EmployeeDTO>? Employees { get; set; } = [];
}