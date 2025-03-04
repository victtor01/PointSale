using PointSaleApi.Src.Core.Domain;
using System;
using System.Linq;

namespace PointSaleApi.Src.Infra.Attributes
{
  // Classe base para os atributos de permissão
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
  public abstract class PermissionAttribute : Attribute
  {
    public string[] Permissions { get; }

    // Construtor que aceita uma lista de permissões
    protected PermissionAttribute(params string[] permissions)
    {
      Permissions = permissions;
    }
  }

  // Atributo para permissões de pedidos (Orders)
  [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
  public class PermissionsOrdersAttribute : PermissionAttribute
  {
    // Construtor que aceita um array de permissões de tipo EmployeePermissionOrders
    public PermissionsOrdersAttribute(params EmployeePermissionOrders[] permissions)
      : base(permissions.Select(p => p.ToString()).ToArray()) // Converte o enum para string
    {
    }
  }

  // Atributo para permissões de loja (Stores)
  [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
  public class PermissionsStoresAttribute : PermissionAttribute
  {
    // Construtor que aceita permissões do tipo EmployeePermissionsProducts
    public PermissionsStoresAttribute(params EmployeePermissionsProducts[] permissions)
      : base(permissions.Select(p => p.ToString()).ToArray()) // Converte o enum para string
    {
    }
  }
}