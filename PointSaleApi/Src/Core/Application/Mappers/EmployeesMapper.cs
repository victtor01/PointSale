using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class EmployeesMapper
{
  public static EmployeeDTO ToMapper(this Employee employee)
  {
    return new EmployeeDTO()
    {
      Id = employee.Id,
      FirstName = employee.FirstName,
      LastName = employee?.LastName,
      Username = employee?.Username,
      Email = employee?.Email,
      Phone = employee?.Phone,
    };
  }
}