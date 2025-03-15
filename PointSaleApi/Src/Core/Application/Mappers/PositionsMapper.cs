using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class PositionsMapper
{
  public static EmployeePositionDTO ToEmployeePositionDTO(this EmployeePosition employeePosition)
  {
    return new EmployeePositionDTO()
    {
      Id = employeePosition.Id,
      Name = employeePosition.Name,
      Permissions = employeePosition.Permissions,
      Employees = employeePosition?.Employees?
        .Select(e => e.ToMapper())
        .ToList()
    };
  }
}