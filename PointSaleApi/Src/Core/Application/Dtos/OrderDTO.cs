using System.ComponentModel.DataAnnotations;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Infra.Attributes;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class OrderDTO
{
  public required Guid TableId { get; set;  }
  public OrderStatus? OrderStatus { get; set; }
}