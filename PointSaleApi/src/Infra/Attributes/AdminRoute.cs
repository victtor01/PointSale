using Microsoft.AspNetCore.Authorization;

namespace PointSaleApi.src.Infra.Attributes
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class IsAdminRoute : Attribute { }
}
