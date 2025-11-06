namespace OrderProcessing.Abstractions;

public interface INotificationService
{
    void Send(string message);
}