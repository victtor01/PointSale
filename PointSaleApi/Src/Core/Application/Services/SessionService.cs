using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

public class SessionService(
  IJwtService jwtService,
  IManagersRepository managersRepository,
  IEmployeeRepository employeeRepository
) : ISessionService
{
  private readonly IEmployeeRepository _employeeRepository = employeeRepository;
  private readonly IManagersRepository _managersRepository = managersRepository;

  public async Task<SessionEmployee> CreateSessionEmployee(int username)
  {
    Employee? employee = await _employeeRepository.FindByUsernameAsyncWithPositions(username);

    if (employee == null)
    {
      Logger.Error("employee não existe.");
      throw new BadRequestException("employee not found");
    }

    return new SessionEmployee()
    {
      Username = employee.Username,
      StoreId = employee.StoreId,
      ManagerId = employee.ManagerId,
      Positions = employee.Positions,
    };
  }

  public async Task<SessionManager> CreateSessionManager(Guid managerId)
  {
    Manager? manager = await _managersRepository.FindByIdAsync(managerId);
    if (manager == null)
    {
      Logger.Error("Manager não existe");
      throw new BadRequestException("user not found!");
    }

    return new SessionManager()
    {
      UserId = manager.Id,
      Email = manager.Email,
    };
  }
 
  public JwtTokensDTO CreateTokensManager(string userId, string email)
  {
    Claim[] claims =
    [
      new(ClaimsKeySessionManager.UserId, userId),
      new(ClaimsKeySessionManager.Email, email),
      new(ClaimsKeySessionManager.Role, UserRole.ADMIN.ToString())
    ];

    return GenerateTokens(claims);
  }

  public JwtTokensDTO CreateTokensEmployee(int username)
  {
    Claim[] claims =
    [
      new(ClaimsKeySessionEmployee.Username, username.ToString()),
      new(ClaimsKeySessionEmployee.Role, UserRole.EMPLOYEE.ToString()),
    ];

    return GenerateTokens(claims);
  }

  private JwtTokensDTO GenerateTokens(Claim[] claims)
  {
    claims = claims.Append(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())).ToArray();

    DateTime expiration = DateTime.UtcNow.AddMinutes(1);
    DateTime longerExpiration = DateTime.UtcNow.AddMinutes(20);

    var token = jwtService.GenerateToken(claims, expiration);
    var refreshToken = jwtService.GenerateToken(claims, longerExpiration);

    return new JwtTokensDTO
    {
      AccessToken = jwtService.ParseJwtTokenToString(token),
      RefreshToken = jwtService.ParseJwtTokenToString(refreshToken)
    };
  }
}