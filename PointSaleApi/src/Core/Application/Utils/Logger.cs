using Serilog;

namespace PointSaleApi.src.Core.Application.Utils
{
  public static class Logger
  {
    static Logger()
    {
      Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
    }

    public static void Information(string message)
    {
      Log.Information(message);
    }

    public static void Warning(string message)
    {
      Log.Warning(message);
    }

    public static void Error(string message)
    {
      Log.Error(message);
    }

    public static void Error(string message, Exception exception)
    {
      Log.Error(exception, message);
    }

    public static void Debug(string message)
    {
      Log.Debug(message);
    }

    public static void Fatal(string message)
    {
      Log.Fatal(message);
    }

    public static void CloseAndFlush()
    {
      Log.CloseAndFlush();
    }
  }
}
