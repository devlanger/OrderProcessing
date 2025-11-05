namespace OrderProcessing.Abstractions;

public interface IOrderService
{
    Task ProcessOrderAsync(int orderId);
}