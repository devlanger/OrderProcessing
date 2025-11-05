using System.Collections.Concurrent;

namespace OrderProcessing.Infrastructure;

public class OrderDatabase
{
    public ConcurrentDictionary<int, Order> Orders { get; set; } = new ConcurrentDictionary<int, Order>();

    public void SeedData()
    {
        Orders.TryAdd(1, new Order() { Id = 1, Description = "Laptop" });
        Orders.TryAdd(1, new Order() { Id = 2, Description = "Phone" });
    }
}