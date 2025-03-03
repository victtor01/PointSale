using PointSaleApi.Src.Core.Application.Enums;

namespace PointSaleApi.Src.Core.Domain;

public class SessionEmployee : Session
{
  public required int Username { get; set; }
  public required Guid ManagerId { get; set; }
  
  public required List<EmployeePosition> Positions { get; set; }

  public SessionEmployee()
  {
    Role = UserRole.EMPLOYEE;
  }
}