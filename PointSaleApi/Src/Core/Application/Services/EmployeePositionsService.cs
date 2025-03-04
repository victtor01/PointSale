using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Services;

public class EmployeePositionsService(IEmployeeRepository employeeRepository) : IEmployeePositionsService
{
  private readonly IEmployeeRepository _employeeRepository = employeeRepository;

  public async Task<EmployeePosition> CreateAsync(CreateEmployeePositionDTO createEmployeePositionDto, Guid ManagerId)
  {
    EmployeePosition employeePosition = new()
    {
      Name = createEmployeePositionDto.Name,
      ManagerId = ManagerId,
    };
    
    employeePosition.SetPermissions(createEmployeePositionDto.Permissions);
    
    return employeePosition;
  }
}