using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Services;

public class PermissionsService : IPermissionsService
{
  private static readonly List<PermissionInformation> Descriptions = new()
  {
    new PermissionInformation(EmployeePermissionOrders.CREATE_ORDER.ToString(),
      "Criar Pedido",
      "Com essa permissão o usuário poderá criar novos pedidos."),

    new PermissionInformation(EmployeePermissionOrders.UPDATE_ORDER_CURRENT.ToString(),
      "Atualizar para EM ANDAMENTO",
      "O funcionário poderá atualizar a pedido para EM ANDAMENTO."),

    new PermissionInformation(EmployeePermissionOrders.UPDATE_ORDER_CANCELLED.ToString(),
      "cancelar pedido.",
      "Com essa permissão o funcionário pode cancelar o pedido."),
  };

  public List<PermissionInformation> GetAllPermissions() => Descriptions;
}