using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class PermissionsOrdersAttribute : Attribute
{
  public List<EmployeePermissionOrders> Permissions { get; }

  public PermissionsOrdersAttribute(params EmployeePermissionOrders[] permissions)
  {
    Permissions = permissions.ToList();
  }
}

// [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
// public class PermissionStoresAttribute : Attribute
// {
//   public List<EmployeePermissionStores> Permissions { get; }
//
//   public PermissionStoresAttribute(params EmployeePermissionStores[] permissions)
//   {
//     Permissions = permissions.ToList();
//   }
// }