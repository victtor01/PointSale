namespace PointSaleApi.Src.Core.Application.Dtos;

public class EmployeeDTO
{
  public Guid Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public int? Username { get; set; }
  public string? LastName { get; set; } = string.Empty;
  public string? Email { get; set; } = string.Empty;
  public string? Phone { get; set; } = string.Empty;
  public List<EmployeePositionDTO>? Positions { get; set; } = [];
}