using Moq;
using OrderProcessing.Abstractions;
using OrderProcessing.Application;

namespace OrderProcessing.Tests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<ILogger> _loggerMock;
    private readonly IOrderService _sut;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _loggerMock = new Mock<ILogger>();
        
        _sut = new OrderService(_orderRepositoryMock.Object, _loggerMock.Object);
    }
    
    [Fact]
    public async Task ProcessOrderAsync_ValidOrder_ShouldLogAndCallRepository()
    {
        //Arrange
        const int orderId = 1;
        
        _orderRepositoryMock
            .Setup(x => x.GetOrderAsync(orderId))
            .ReturnsAsync("Test description");
        
        //Act
        await _sut.ProcessOrderAsync(orderId);
        
        //Assert
        _orderRepositoryMock.Verify(x => x.GetOrderAsync(orderId), Times.Once);
        _loggerMock.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Exactly(2));
        _loggerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ProcessOrderAsync_InvalidOrder_ShouldThrowValidationAndLogError()
    {
        //Arrange
        const int orderId = -1;
        _orderRepositoryMock
            .Setup(r => r.GetOrderAsync(orderId))
            .ThrowsAsync(new ArgumentException());
        
        //Act
        await _sut.ProcessOrderAsync(orderId);
        
        //Assert
        _orderRepositoryMock.Verify(x => x.GetOrderAsync(orderId), Times.Never);
        _loggerMock.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Once);
        _loggerMock.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<ArgumentException>()), Times.Once);
    }
    
    
    [Fact]
    public async Task ProcessOrderAsync_InvalidOrder_NotFound()
    {
        //Arrange
        const int orderId = 999;
        _orderRepositoryMock
            .Setup(r => r.GetOrderAsync(orderId))
            .ThrowsAsync(new KeyNotFoundException());
        
        //Act
        await _sut.ProcessOrderAsync(orderId);
        
        //Assert
        _orderRepositoryMock.Verify(x => x.GetOrderAsync(orderId), Times.Once);
        _loggerMock.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Once);
        _loggerMock.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<KeyNotFoundException>()), Times.Once);
    }
}