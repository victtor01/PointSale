namespace PointSaleApi.src.Infra.Config
{
  public class ErrorInstance(string message, int statusCode = 500) : Exception(message)
  {
    public int StatusCode { get; } = statusCode;
    public string Error { get; set; } = "Internal Server Error"; // Mensagem padrão
  }

  public class BadRequestException : ErrorInstance
  {
    public BadRequestException(string message)
      : base(message, 400)
    {
      Error = "Bad Request";
    }
  }

  public class NotFoundException : ErrorInstance
  {
    public NotFoundException(string message)
      : base(message, 404)
    {
      Error = "Not Found";
    }
  }

  public class UnauthorizedException : ErrorInstance
  {
    public UnauthorizedException(string message)
      : base(message, 401)
    {
      Error = "Unauthorized";
    }
  }
}
