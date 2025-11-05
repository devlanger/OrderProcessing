using OrderProcessing.Abstractions;

namespace OrderProcessing.Infrastructure;

public class OrderRepository(OrderDatabase database) : IOrderRepository
{
    public async Task<string> GetOrderAsync(int orderId)
    {
        await Task.Delay(100);

        if(orderId <= 0)
            throw new ArgumentException("Invalid order id", nameof(orderId));

        if (database.Orders.TryGetValue(orderId, out var order))
            return order.Description;

        throw new KeyNotFoundException($"Order with id {orderId} not found");
    }
}