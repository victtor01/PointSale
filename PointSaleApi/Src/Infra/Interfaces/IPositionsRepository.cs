using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Interfaces;

public interface IPositionsRepository
{
  public Task<EmployeePosition> Create(EmployeePosition employeePosition);
  public Task<EmployeePosition?> GetById(Guid id);
  public Task<List<EmployeePosition>> FindAllByIds(List<Guid> ids);
  public Task<List<EmployeePosition>> FindAllByManagerAndStoreIdAsync(Guid managerId, Guid storeId);
  public Task<EmployeePosition?> FindByNameAndManagerAsync(string name, Guid managerId);
  public Task<EmployeePosition> Update(EmployeePosition employeePosition);
}