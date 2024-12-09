using System.Net;
using System.Text.Json;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Infra.Api.Middlewares
{
  public class ErrorMiddleware(RequestDelegate next)
  {
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        Console.WriteLine("PASSOU NO ERRORMIDDLEARE");
        await _next(context);
      }
      catch (Exception ex)
      {
        Logger.Error("PASSOU DENOVO NO ERRORMIDDLEWARE");
        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      var response = context.Response;
      response.ContentType = "application/json";

      if (exception is ErrorInstance errorInstance)
      {
        response.StatusCode = errorInstance.StatusCode;
        string result = JsonSerializer.Serialize(
          new
          {
            type = errorInstance.Type,
            message = errorInstance.Message,
            errors = errorInstance.Errors,
          }
        );

        return response.WriteAsync(result);
      }
      else
      {
        Logger.Fatal(exception.Message);
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var defaultResult = JsonSerializer.Serialize(
          new { error = "Internal Server Error", message = "Houve um erro desconhecido!" }
        );
        return response.WriteAsync(defaultResult);
      }
    }
  }
}
