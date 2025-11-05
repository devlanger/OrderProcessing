namespace OrderProcessing.Abstractions;

public interface IOrderRepository
{
    void AddOrder(Order order);
    
    Task<string> GetOrderAsync(int orderId);
}