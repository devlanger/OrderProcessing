public class ConsoleLogger : ILogger
{
    private static DateTimeOffset GetLogDateTime() => DateTimeOffset.UtcNow;

    public void LogInfo(string message)
    {
        Console.WriteLine($"[{GetLogDateTime()}] - {message}");
    }

    public void LogError(string message, Exception ex)
    {
        Console.WriteLine($"[{GetLogDateTime()}] - {message}");
    }
}