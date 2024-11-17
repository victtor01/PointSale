using PointSaleApi.src.Core.Application.Dtos.JwtDtos;

namespace PointSaleApi.src.Core.Application.Interfaces.SessionInterfaces
{
  public interface ISessionService
  {
    public JwtTokensDto CreateSessionUser(string userId, string email, string role);
  }
}
