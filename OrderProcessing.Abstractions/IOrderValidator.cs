namespace OrderProcessing.Abstractions;

public interface IOrderValidator
{
    bool IsValid(int orderId);
}