using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces
{
  public interface IManagersRepository
  {
    public Task<Manager> Save(Manager manager);
    public Task<Manager?> FindByEmailAsync(string email);
    public Task<Manager?> FindByIdAsync(Guid managerId);
  }
}
