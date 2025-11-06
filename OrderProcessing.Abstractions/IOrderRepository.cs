namespace OrderProcessing.Abstractions;

public interface IOrderRepository
{
    void AddOrder(Order order);
    
    Task<Order> GetOrderAsync(int orderId);
}