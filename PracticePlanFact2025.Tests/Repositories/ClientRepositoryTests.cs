using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Repositories.Implementations;
using Data;

namespace PracticePlanFact2025.Tests.Repositories
{
    public class ClientRepositoryTests
    {
        private readonly List<Client> _clients = new()
        {
            new Client { Id = Guid.NewGuid(), FirstName = "Иван", LastName = "Иванов", DateOfBirth = new DateOnly(1990, 1, 1) },
            new Client { Id = Guid.NewGuid(), FirstName = "Петр", LastName = "Петров", DateOfBirth = new DateOnly(1985, 5, 15) },
        };

        private AppDBContext GetFakeContext()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDBContext(options);
            context.Clients.AddRange(_clients);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllClients()
        {
            // Arrange (подготовка)
            var context = GetFakeContext();
            var repository = new ClientRepository(context);

            // Act (действие)
            var (clients, count) = await repository.GetAllAsync();

            // Assert (проверка)
            Assert.Equal(2, count);
            Assert.Equal(2, clients.Count());
        }
    }
}
