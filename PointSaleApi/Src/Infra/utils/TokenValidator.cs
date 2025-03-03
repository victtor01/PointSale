using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;
using PointSaleApi.Src.Infra.Interfaces;

namespace PointSaleApi.Src.Infra.utils;

public class TokenValidator(IJwtService jwtService, ISessionService sessionService) : ITokenValidator
{
  private readonly IJwtService _jwtService = jwtService;
  private readonly ISessionService _sessionService = sessionService;

  public Dictionary<string, string> VerifyAndRenewTokenAsync(JwtTokensDTO tokens, HttpResponse response)
  {
    try
    {
      string accessToken = tokens.AccessToken;
      return _jwtService.VerifyTokenAndGetClaims(accessToken);
    }
    catch (BadRequestException)
    {
      string refreshToken = tokens.RefreshToken;
      var refreshTokenClaims = _jwtService.VerifyTokenAndGetClaims(refreshToken);

      UserRole role = Enum.Parse<UserRole>(refreshTokenClaims[ClaimsKeySessionManager.Role]);

      JwtTokensDTO newTokens;

      switch (role)
      {
        case UserRole.EMPLOYEE:
          string username = refreshTokenClaims[ClaimsKeySessionEmployee.Username];

          newTokens = _sessionService.CreateTokensEmployee(
            username: username.ToIntOrThrow()
          );
          break;

        case UserRole.ADMIN:
          newTokens = _sessionService.CreateTokensManager(
            userId: refreshTokenClaims[ClaimsKeySessionManager.UserId],
            email: refreshTokenClaims[ClaimsKeySessionManager.Email]
          );
          break;

        default:
          throw new BadRequestException("Invalid role.");
      }

      Dictionary<string, string> accessTokenClaims = _jwtService.VerifyTokenAndGetClaims(newTokens.AccessToken);

      response.Cookies.Append(
        key: CookiesSessionKeys.AccessToken,
        value: newTokens.AccessToken,
        options: new CookieOptions { HttpOnly = true }
      );

      return accessTokenClaims;
    }
    catch (Exception e)
    {
      Logger.Fatal(e.Message);
      Logger.Fatal(e.Message);
      throw new UnauthorizedException("A sessão está inválida");
    }
  }

  public Guid GetStoreInToken(string sessionStoreToken)
  {
    try
    {
      Dictionary<string, string> sessionStorePayload =
        this._jwtService.VerifyTokenAndGetClaims(sessionStoreToken);

      string storeIdString = sessionStorePayload.TryGetValue(ClaimsKeySessionManager.Store, out string? str)
        ? str
        : throw new BadRequestException("token ja loja não presente!");

      Guid storeId = storeIdString.ToGuidOrThrow();

      return storeId;
    }
    catch (Exception e)
    {
      Logger.Error("Houve um erro", e);
      throw new BadRequestException("Selecione a loja novamente!");
    }
  }
}