namespace PointSaleApi.Src.Core.Application.Dtos;

public class JwtTokensDTO
{
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
}