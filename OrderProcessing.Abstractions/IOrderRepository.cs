namespace OrderProcessing.Abstractions;

public interface IOrderRepository
{
    string GetOrder(int orderId);
}