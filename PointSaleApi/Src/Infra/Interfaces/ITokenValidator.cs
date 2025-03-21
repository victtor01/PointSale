using PointSaleApi.Src.Core.Application.Dtos;

namespace PointSaleApi.Src.Infra.Interfaces;

public interface ITokenValidator
{
  public Dictionary<string, string> VerifyAndRenewTokenAsync(JwtTokensDTO tokens, HttpResponse response);
  public Guid GetStoreInToken(string sessionStoreToken);
}