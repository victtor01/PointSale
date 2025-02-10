using System.Text.Json.Serialization;

namespace PointSaleApi.Src.Core.Application.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
  PAID,
  CANCELLED,
  CURRENT,
}