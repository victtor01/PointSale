using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class IsAdminRoute : TypeFilterAttribute
{
  public IsAdminRoute() : base(typeof(IsAdminFilter)) { }

  private class IsAdminFilter : IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      if (context.HttpContext.GetManagerSessionOrThrow()?.Role == null)
      {
        throw new BadRequestException("TTESTE");
      }
    }
  }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class IsStoreSelectedRoute : Attribute { }