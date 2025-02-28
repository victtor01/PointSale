using Newtonsoft.Json;

namespace PointSaleApi.Src.Infra.Extensions;

public static class ObjectExtension
{
  public static void LoggerJson(this Object obj)
  {
    string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
    Console.WriteLine(json);
  }
  
}