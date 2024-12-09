using Microsoft.AspNetCore.Authorization;

namespace PointSaleApi.Src.Infra.Attributes
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class IsAdminRoute : Attribute { }
}
