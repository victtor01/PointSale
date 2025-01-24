using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces
{
  public interface IManagersRepository
  {
    public Task<Manager> SaveAsync(Manager manager);
    public Task<Manager?> FindByEmailAsync(string email);
    public Task<Manager?> FindByIdAsync(Guid managerId);
  }
}