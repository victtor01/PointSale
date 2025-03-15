using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.utils;

namespace PointSaleApi.Src.Infra.Extensions;

public static class SessionExtensions
{
  public static void ValidateUserRoleForRoute(this Session session, HttpContext httpContext)
  {
    if (RouteValidator.IsAdminRoute(httpContext) && session.Role != UserRole.ADMIN)
      throw new BadRequestException("Usuário não tem permissão!");

    if (RouteValidator.IsEmployeeRoute(httpContext) && session.Role != UserRole.EMPLOYEE)
      throw new BadRequestException("Usuário não tem permissão para acessar métodos do employee!");
  }
}