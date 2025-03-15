using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IEmployeePositionsService
{
  public Task<List<EmployeePosition>> GetAllAsync(Guid ManagerId, Guid storeId);
  public Task<EmployeePosition> GetByIdAsync(Guid managerId, Guid positionId);

  public Task<EmployeePosition> UpdateAsync(Guid managerId, Guid positionId,
    UpdateEmployeePositionRecord updateEmployeePositionRecord);

  public Task<EmployeePosition> CreateAsync(
    CreateEmployeePositionDTO createEmployeePositionDto,
    Guid ManagerId, Guid storeId);
}