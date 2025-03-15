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
      "Atualizar ordem para (em andamento)",
      "Com essa permissão é possível colocar a ordem para (pendente)"),
    
    new PermissionInformation(EmployeePermissionOrders.UPDATE_ORDER_PAID.ToString(),
      "Atualizar para (pago)",
      "Com essa permissão o funcionário poderá aceitar pagamentos sobre a ordem"),

    new PermissionInformation(EmployeePermissionOrders.UPDATE_ORDER_CANCELLED.ToString(),
      "cancelar pedido.",
      "Com essa permissão o funcionário pode cancelar o pedido."),
  };

  public List<PermissionInformation> GetAllPermissions() => Descriptions;
}