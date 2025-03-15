using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IPermissionsService
{
  public List<PermissionInformation> GetAllPermissions();
}