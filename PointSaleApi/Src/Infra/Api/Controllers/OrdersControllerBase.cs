using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using System.Threading.Tasks;

namespace PointSaleApi.Src.Infra.Api.Controllers;

public abstract class OrdersControllerBase : ControllerBase
{
  public abstract Task<IActionResult> Create([FromBody] CreateOrderDTO orderDTO);
  public abstract Task<IActionResult> FindAll();
}