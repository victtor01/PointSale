using PointSaleApi.src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces
{
  public interface IManagersRepository
  {
    public Task<Manager> Save(Manager manager);
    public Task<Manager?> FindByEmailAsync(string email);
  }
}
