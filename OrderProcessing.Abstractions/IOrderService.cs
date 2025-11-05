namespace OrderProcessing.Abstractions;

public interface IOrderService
{
    Task ProcessOrder(int orderId);
}