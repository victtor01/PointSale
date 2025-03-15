namespace PointSaleApi.Src.Core.Application.Records;

public record UpdateEmployeePositionRecord(
  string Name,
  List<string> permissions

);
