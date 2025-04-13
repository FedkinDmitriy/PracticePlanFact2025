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
            var context = GetFakeContext();
            var repository = new ClientRepository(context);

            var (clients, count) = await repository.GetAllAsync();

            Assert.Equal(2, count);
            Assert.Equal(2, clients.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectClient()
        {
            var context = GetFakeContext();
            var repository = new ClientRepository(context);
            var expectedClient = _clients.First();

            var client = await repository.GetByIdAsync(expectedClient.Id);

            Assert.NotNull(client);
            Assert.Equal(expectedClient.Id, client!.Id);
            Assert.Equal(expectedClient.FirstName, client.FirstName);
            Assert.Equal(expectedClient.LastName, client.LastName);
            Assert.Equal(expectedClient.DateOfBirth, client.DateOfBirth);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenClientNotFound()
        {
            var context = GetFakeContext();
            var repository = new ClientRepository(context);
            var fakeId = Guid.NewGuid();

            var client = await repository.GetByIdAsync(fakeId);

            Assert.Null(client);
        }

        [Fact]
        public async Task AddAsync_AddsClientSuccessfully()
        {
            var context = GetFakeContext();
            var repository = new ClientRepository(context);

            var newClient = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "Алексей",
                LastName = "Смирнов",
                DateOfBirth = new DateOnly(1995, 7, 20)
            };

            await repository.AddAsync(newClient);
            var (clients, count) = await repository.GetAllAsync();

            Assert.Equal(3, count); // было 2, стало 3
            Assert.Contains(clients, c => c.FirstName == "Алексей" && c.LastName == "Смирнов");
        }

        [Fact]
        public async Task UpdateAsync_UpdatesClientSuccessfully()
        {
            var context = GetFakeContext();
            var repository = new ClientRepository(context);
            var client = _clients[0];

            client.FirstName = "Иван-Обновлённый";

            await repository.UpdateAsync(client);
            var updated = await repository.GetByIdAsync(client.Id);

            Assert.NotNull(updated);
            Assert.Equal("Иван-Обновлённый", updated!.FirstName);
        }

        [Fact]
        public async Task DeleteAsync_DeletesClientSuccessfully()
        {
            var context = GetFakeContext();
            var repository = new ClientRepository(context);
            var client = _clients[0];

            await repository.DeleteAsync(client.Id);
            var deleted = await repository.GetByIdAsync(client.Id);

            Assert.Null(deleted);
        }

    }
}
