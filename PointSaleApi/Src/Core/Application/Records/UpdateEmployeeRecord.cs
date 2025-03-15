namespace PointSaleApi.Src.Core.Application.Records;

public record UpdateEmployeeRecord(
  string FirstName,
  string LastName,
  string Email,
  string Phone,
  decimal Salary,
  List<Guid> Positions
);