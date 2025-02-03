using PointSaleApi.Src.Core.Application.Dtos;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface ISessionService
{
  public JwtTokensDTO CreateSessionUser(string userId, string email, string role);
}