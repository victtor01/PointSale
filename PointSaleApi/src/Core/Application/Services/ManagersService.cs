using PointSaleApi.Src.Core.Application.Dtos.ManagersDtos;
using PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services
{
  public class ManagersService(IManagersRepository managersRepository) : IManagersService
  {
    private readonly IManagersRepository _managersRepository = managersRepository;

    public async Task<Manager> FindByEmailOrThrowAsync(string email)
    {
      Manager manager =
        await _managersRepository.FindByEmailAsync(email)
        ?? throw new NotFoundException("user not found!");

      return manager;
    }

    public async Task<Manager> RegisterAsync(CreateUserDto createUserDto)
    {
      var row = await _managersRepository.FindByEmailAsync(createUserDto.Email);
      if (row != null)
        throw new UnauthorizedException("user already exists");

      var manager = new Manager { Name = createUserDto.Name, Email = createUserDto.Email };
      manager.HashAndSetPassword(manager.Id.ToString(), createUserDto.Password);

      var saved = await _managersRepository.Save(manager);

      return saved;
    }

    public Task<Manager> Save()
    {
      throw new NotImplementedException();
    }
  }
}
