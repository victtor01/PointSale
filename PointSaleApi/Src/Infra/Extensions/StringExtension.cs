using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Infra.Extensions;

public static class StringExtension
{
  public static Guid ToGuidOrThrow(this string value, string errorMessage = "GUID is not valid")
  {
    if (Guid.TryParse(value, out Guid result))
    {
      return result;
    }

    throw new BadRequestException(errorMessage);
  }
}