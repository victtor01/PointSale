using System.Text.Json.Serialization;

namespace PointSaleApi.Src.Core.Application.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderProductStatus
{
  PENDING = 0,
  IN_PROGRESS = 1,
  READY = 2,
  DELIVERED = 3,
  CANCELED = 4
}