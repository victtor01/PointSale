using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class PermissionsOrdersAttribute : Attribute
{
  public List<string> Permissions { get; }

  public PermissionsOrdersAttribute(params string[] permissions)
  {
    Permissions = permissions.ToList();
  }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class PermissionStoresAttribute : Attribute
{
  public List<string> Permissions { get; }

  public PermissionStoresAttribute(params string[] permissions)
  {
    Permissions = permissions.ToList();
  }
}