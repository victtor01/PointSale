using PointSaleApi.Src.Infra.Attributes;

namespace PointSaleApi.Src.Infra.utils;

public static class RouteValidator
{
  public static bool IsStoreSelectedRoute(HttpContext context)
  {
    var endpoint = context.GetEndpoint();
    return endpoint?.Metadata?.GetMetadata<IsStoreSelectedRoute>() != null;
  }

  public static bool IsPublicRoute(HttpContext context)
  {
    var endpoint = context.GetEndpoint();
    return endpoint?.Metadata?.GetMetadata<IsPublicRoute>() != null;
  }

  public static bool IsAdminRoute(HttpContext context)
  {
    var endpoint = context.GetEndpoint();
    return endpoint?.Metadata?.GetMetadata<IsAdminRoute>() != null;
  }

  public static bool IsEmployeeRoute(HttpContext context)
  {
    var endpoint = context.GetEndpoint();
    return endpoint?.Metadata?.GetMetadata<IsEmployeeRoute>() != null;
  }
}