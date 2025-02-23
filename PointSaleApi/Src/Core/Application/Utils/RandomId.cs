namespace PointSaleApi.Src.Core.Application.Utils;

public static class RandomId
{
  public static int Generate()
  {
    long timestamp = DateTime.UtcNow.Ticks;
    int randomSeed = (int)(timestamp % int.MaxValue);
    Random random = new Random(randomSeed);

    return random.Next(10000, 999999);
  }
}