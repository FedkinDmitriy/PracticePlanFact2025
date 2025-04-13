using Data.Models;
using Data.Models.Enums;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace PracticePlanFact2025.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _controller = new OrdersController(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOrdersWithPagination()
        {
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), OrdersDateTime = DateTime.UtcNow, Status = OrderStatus.Не_обработан },
                new Order { Id = Guid.NewGuid(), OrdersDateTime = DateTime.UtcNow, Status = OrderStatus.Выполнен }
            };

            _orderRepositoryMock
                .Setup(repo => repo.GetAllAsync(null, null, null, 1, 10))
                .ReturnsAsync((orders, orders.Count));

            var result = await _controller.GetAll(null, null, null, 1, 10) as OkObjectResult;
            var response = result?.Value as dynamic;

            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsOrder()
        {
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId, OrdersDateTime = DateTime.UtcNow, Status = OrderStatus.Не_обработан };

            _orderRepositoryMock
                .Setup(repo => repo.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _controller.GetById(orderId) as OkObjectResult;
            var response = result?.Value as Order;

            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);
            Assert.Equal(orderId, response?.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfOrderDoesNotExist()
        {
            var orderId = Guid.NewGuid();

            _orderRepositoryMock
                .Setup(repo => repo.GetByIdAsync(orderId))
                .ReturnsAsync((Order?)null);

            var result = await _controller.GetById(orderId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            var order = new Order { Id = Guid.NewGuid(), OrdersDateTime = DateTime.UtcNow, Status = OrderStatus.Не_обработан };

            _orderRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Create(order) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result?.StatusCode);
            Assert.Equal("GetById", result?.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId, OrdersDateTime = DateTime.UtcNow, Status = OrderStatus.Не_обработан };

            _orderRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Update(orderId, order);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            var order = new Order { Id = Guid.NewGuid(), OrdersDateTime = DateTime.UtcNow };

            var result = await _controller.Update(Guid.NewGuid(), order);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId, OrdersDateTime = DateTime.UtcNow };

            _orderRepositoryMock
                .Setup(repo => repo.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            _orderRepositoryMock
                .Setup(repo => repo.DeleteAsync(orderId))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(orderId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var orderId = Guid.NewGuid();

            _orderRepositoryMock
                .Setup(repo => repo.GetByIdAsync(orderId))
                .ReturnsAsync((Order?)null);

            var result = await _controller.Delete(orderId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
