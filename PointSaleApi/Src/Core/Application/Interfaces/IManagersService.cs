using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IManagersService
{
  public Task<Manager> Save();
  public Task<Manager> FindByIdOrThrowAsync(Guid guid);
  public Task<Manager> RegisterAsync(CreateUserDTO createUserDto);
  public Task<Manager> FindByEmailOrThrowAsync(string Email);
}