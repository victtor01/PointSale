using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services
{
  public class JwtService(
    IConfiguration configuration,
    TokenValidationParameters validationParameters
  ) : IJwtService
  {
    private readonly IConfiguration _configuration = configuration;
    private readonly TokenValidationParameters _validationParameters = validationParameters;

    public Dictionary<string, string> VerifyTokenAndGetClaims(string token)
    {
      try
      {
        var handler = new JwtSecurityTokenHandler();

        var valid = handler.ValidateToken(
          token,
          _validationParameters,
          out SecurityToken validatedToken
        );

        var claims = valid.Claims.ToDictionary(c => c.Type, c => c.Value);

        return claims;
      }
      catch (Exception e)
      {
        Logger.Error(e.Message);
        throw new BadRequestException("The session is not valid!");
      }
    }

    public JwtSecurityToken GenerateToken(Claim[] claims, DateTime expiration)
    {
      var privateKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]!)
      );

      var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        issuer: "teste",
        audience: "teste",
        expires: expiration,
        signingCredentials: credentials,
        claims: claims
      );

      return token;
    }

    public string ParseJwtTokenToString(JwtSecurityToken token)
    {
      string stringToken = new JwtSecurityTokenHandler().WriteToken(token);

      return stringToken;
    }
  }
}
