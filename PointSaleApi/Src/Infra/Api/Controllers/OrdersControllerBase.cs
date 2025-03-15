using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using System.Threading.Tasks;
using PointSaleApi.Src.Core.Application.Records;

namespace PointSaleApi.Src.Infra.Api.Controllers;

public abstract class OrdersControllerBase : ControllerBase
{
  public abstract Task<IActionResult> CreateAsync(CreateOrderDTO orderDTO);
  public abstract Task<IActionResult> FindAll();
}