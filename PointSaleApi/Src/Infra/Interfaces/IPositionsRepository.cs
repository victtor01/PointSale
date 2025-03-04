using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Interfaces;

public interface IPositionsRepository
{
  public Task<EmployeePosition> Create(EmployeePosition employeePosition);
  public Task<EmployeePosition?> GetById(Guid id);
}