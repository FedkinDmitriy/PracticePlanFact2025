using Data;
using Data.Models;
using Data.Models.Enums;
using Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace PracticePlanFact2025.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Guid _clientId = Guid.NewGuid();

        private AppDBContext GetFakeContext()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDBContext(options);

            context.Clients.Add(new Client
            {
                Id = _clientId,
                FirstName = "Тест",
                LastName = "Клиент",
                DateOfBirth = new DateOnly(1990, 1, 1)
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task AddAsync_ThenGetAllAsync_ReturnsAddedOrder()
        {
            var context = GetFakeContext();
            var repository = new OrderRepository(context);

            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = _clientId,
                OrderSum = 150.0m,
                OrdersDateTime = DateTime.UtcNow,
                Status = OrderStatus.Выполнен
            };

            await repository.AddAsync(newOrder);
            var (orders, count) = await repository.GetAllAsync();

            Assert.Single(orders);
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task UpdateAsync_ChangesOrderCorrectly()
        {
            var context = GetFakeContext();
            var repository = new OrderRepository(context);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = _clientId,
                OrderSum = 200.0m,
                OrdersDateTime = DateTime.UtcNow,
                Status = OrderStatus.Отменен
            };

            await repository.AddAsync(order);

            order.OrderSum = 300.0m;
            order.Status = OrderStatus.Выполнен;

            await repository.UpdateAsync(order);

            var updated = await repository.GetByIdAsync(order.Id);
            Assert.Equal(300.0m, updated?.OrderSum);
            Assert.Equal(OrderStatus.Выполнен, updated?.Status);
        }

        [Fact]
        public async Task DeleteAsync_RemovesOrder()
        {
            var context = GetFakeContext();
            var repository = new OrderRepository(context);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = _clientId,
                OrderSum = 100.0m,
                OrdersDateTime = DateTime.UtcNow,
                Status = OrderStatus.Не_обработан
            };

            await repository.AddAsync(order);
            await repository.DeleteAsync(order.Id);

            var deleted = await repository.GetByIdAsync(order.Id);
            Assert.Null(deleted);
        }
    }
}
