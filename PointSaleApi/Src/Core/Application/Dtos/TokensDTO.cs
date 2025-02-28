namespace PointSaleApi.Src.Core.Application.Dtos;

public class TokensManagerDTO : JwtTokensDTO
{
  public string? StoreToken { get; set; } = string.Empty;
}