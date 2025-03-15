using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Infra.Extensions;

public static class EmployeeSessionExtensions
{
  public static bool HasPermissionsOrThrow(this SessionEmployee session, string permissionRequired)
  {
    foreach (var position in session.Positions)
    {
      if (position.Permissions.Contains(permissionRequired))
        return true;
    }

    throw new UnauthorizedException("employee does not have necessary permissions");
  }
}