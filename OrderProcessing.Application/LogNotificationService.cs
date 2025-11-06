using OrderProcessing.Abstractions;

namespace OrderProcessing.Application;

public class LogNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"[Notification]({DateTimeOffset.UtcNow}): {message}");
    }
}