using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointSaleApi.src.Core.Application.Dtos.ManagersDtos;
using PointSaleApi.src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces
{
  public interface IManagersService
  {
    public Task<Manager> Save();
    public Task<Manager> Register(CreateUserDto createUserDto);
    public Task<Manager> FindByEmailOrThrowAsync(string Email);
  }
}
