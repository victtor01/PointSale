namespace PointSaleApi.Src.Core.Application.Dtos.JwtDtos
{
  public class JwtTokensDto
  {
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string? StoreToken { get; set; } = string.Empty;
  }
}
