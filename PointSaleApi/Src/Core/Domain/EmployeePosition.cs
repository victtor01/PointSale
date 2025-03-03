using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointSaleApi.Src.Core.Domain;

[Table("employee_position")]
public class EmployeePosition
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [Column("name")]
  [MaxLength(100)]
  public required string Name { get; set; }
}

public enum EmployeePermission
{
  CREATE_ORDER,       // Criar pedidos
  PROCESS_PAYMENT,    // Processar pagamentos
  MANAGE_EMPLOYEES,   // Gerenciar funcionários
  VIEW_REPORTS,       // Visualizar relatórios
  MANAGE_STOCK        // Gerenciar estoque
}