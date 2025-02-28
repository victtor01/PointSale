using Serilog;
using System.Runtime.CompilerServices;

namespace PointSaleApi.Src.Core.Application.Utils;

public static class Logger
{
    static Logger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public static void Information(string message, 
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string memberName = "")
    {
        Log.Information("{File} - {Method}: {Message}", GetFileName(filePath), memberName, message);
    }

    public static void Warning(string message, 
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string memberName = "")
    {
        Log.Warning("{File} - {Method}: {Message}", GetFileName(filePath), memberName, message);
    }

    public static void Error(string message, 
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string memberName = "")
    {
        Log.Error("{File} - {Method}: {Message}", GetFileName(filePath), memberName, message);
    }

    public static void Error(string message, Exception exception, 
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string memberName = "")
    {
        Log.Error(exception, "{File} - {Method}: {Message}", GetFileName(filePath), memberName, message);
    }

    public static void Debug(string message, 
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string memberName = "")
    {
        Log.Debug("{File} - {Method}: {Message}", GetFileName(filePath), memberName, message);
    }

    public static void Fatal(string message, 
        [CallerFilePath] string filePath = "", 
        [CallerMemberName] string memberName = "")
    {
        Log.Fatal("{File} - {Method}: {Message}", GetFileName(filePath), memberName, message);
    }

    public static void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }

    private static string GetFileName(string filePath)
    {
        return string.IsNullOrEmpty(filePath) ? "UnknownFile" : Path.GetFileName(filePath);
    }
}