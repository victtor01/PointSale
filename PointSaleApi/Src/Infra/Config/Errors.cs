namespace PointSaleApi.Src.Infra.Config;

public class ErrorInstance(string message, int statusCode = 500) : Exception(message)
{
  public int StatusCode { get; } = statusCode;
  public string Type { get; set; } = "Internal Server Error"; // Mensagem padrão
  public Dictionary<string, string[]?>? Errors;
}

public class BadRequestException : ErrorInstance
{
  public BadRequestException(string message, Dictionary<string, string[]?>? errors = null)
    : base(message, 400)
  {
    Type = "Bad Request";
    Errors = errors ?? [];
  }
}

public class NotFoundException : ErrorInstance
{
  public NotFoundException(string message)
    : base(message, 404)
  {
    Type = "Not Found";
  }
}

public class UnauthorizedException : ErrorInstance
{
  public UnauthorizedException(string message)
    : base(message, 401)
  {
    Type = "Unauthorized";
  }
}