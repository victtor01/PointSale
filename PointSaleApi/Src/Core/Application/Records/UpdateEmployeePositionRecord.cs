namespace PointSaleApi.Src.Core.Application.Records;

public record UpdateEmployeePositionRecord(
  string Name,
  List<string> Permissions,
  List<Guid> Employees
);
