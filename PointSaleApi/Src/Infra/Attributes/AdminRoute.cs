namespace PointSaleApi.Src.Infra.Attributes
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class IsAdminRoute : Attribute { }

  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class IsStoreSelectedRoute : Attribute { }
}
