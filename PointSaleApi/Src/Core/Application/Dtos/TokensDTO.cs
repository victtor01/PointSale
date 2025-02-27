namespace PointSaleApi.Src.Core.Application.Dtos;

public class TokensDTO
{
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
  public string? StoreToken { get; set; } = string.Empty;
}