using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface ISessionService
{
  public JwtTokensDTO CreateTokensManager(string userId, string email, string role);
  
  public JwtTokensDTO CreateTokensEmployee(int username, string role, Guid storeId);

  public Task<SessionEmployee> CreateSessionEmployee(int username);

  public Task<SessionManager> CreateSessionManager(Guid managerId);
}