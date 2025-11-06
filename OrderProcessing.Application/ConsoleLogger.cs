using Microsoft.Extensions.Options;
using OrderProcessing.Abstractions;
using OrderProcessing.Core.Config;

namespace OrderProcessing.Application;

public class ConsoleLogger(IOptions<LoggingConfiguration> logConfig) : ILogger
{
    private static DateTimeOffset GetLogDateTime() => DateTimeOffset.UtcNow;

    public void LogInfo(string message)
    {
        if (logConfig.Value.LogLevel != "Info")
            return;
        
        Console.WriteLine($"[{GetLogDateTime()}] - {message}");
    }

    public void LogError(string message, Exception ex)
    {
        Console.WriteLine($"[{GetLogDateTime()}] - {message}");
    }
}