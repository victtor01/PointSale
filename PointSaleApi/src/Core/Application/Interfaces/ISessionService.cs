using PointSaleApi.Src.Core.Application.Dtos.JwtDtos;

namespace PointSaleApi.Src.Core.Application.Interfaces
{
  public interface ISessionService
  {
    public JwtTokensDto CreateSessionUser(string userId, string email, string role);
  }
}
