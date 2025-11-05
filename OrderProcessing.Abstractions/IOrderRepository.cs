namespace OrderProcessing.Abstractions;

public interface IOrderRepository
{
    Task<string> GetOrderAsync(int orderId);
}