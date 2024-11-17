using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSaleApi.src.Core.Application.Dtos.AuthDtos
{
  public class SelectStoreDto
  {
    public Guid StoreId { get; set; }
    public Guid ManagerId { get; set; }
  }
}
