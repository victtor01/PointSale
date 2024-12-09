using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointSaleApi.Src.Core.Application.Dtos.ManagersDtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces
{
  public interface IManagersService
  {
    public Task<Manager> Save();
    public Task<Manager> RegisterAsync(CreateUserDto createUserDto);
    public Task<Manager> FindByEmailOrThrowAsync(string Email);
  }
}
