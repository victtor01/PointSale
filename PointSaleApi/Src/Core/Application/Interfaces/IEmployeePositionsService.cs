using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IEmployeePositionsService
{
  public Task<EmployeePosition> CreateAsync(CreateEmployeePositionDTO createEmployeePositionDto, Guid ManagerId);
}