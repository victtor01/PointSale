using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class IsEmployeeRoute : TypeFilterAttribute
{
  public IsEmployeeRoute() : base(typeof(IsEmployeeFilter)) { }

  private class IsEmployeeFilter : IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      if (context.HttpContext.GetEmployeeSessionOrThrow()?.Role == null)
      {
        throw new UnauthorizedException("Usuário não tem permissão para acessar essa rota!");
      }
    }
  }
}