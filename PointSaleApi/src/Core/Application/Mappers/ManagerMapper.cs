using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Mappers
{
  public static class ManagerMapper
  {
    public static ManagerDto ToManagerMapper(this Manager manager)
    {
      return new ManagerDto { Email = manager.Email };
    }
  }

  public class ManagerDto
  {
    public required string Email { get; set; }
  }
}
