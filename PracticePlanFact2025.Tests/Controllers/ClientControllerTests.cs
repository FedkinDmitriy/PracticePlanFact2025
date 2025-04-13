using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using Data.Repositories.Interfaces;

namespace PracticePlanFact2025.Tests.Controllers
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly ClientsController _controller;

        public ClientsControllerTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _controller = new ClientsController(_clientRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsClientsWithPagination()
        {
            var clients = new List<Client>
            {
                new Client { Id = Guid.NewGuid(), FirstName = "Иван", LastName = "Иванов", DateOfBirth = new DateOnly(1990, 1, 1) },
                new Client { Id = Guid.NewGuid(), FirstName = "Петр", LastName = "Петров", DateOfBirth = new DateOnly(1985, 5, 15) }
            };
            _clientRepositoryMock
                .Setup(repo => repo.GetAllAsync(null, null, null, 1, 10))
                .ReturnsAsync((clients, clients.Count));

            
            var result = await _controller.GetAll(null, null, null, 1, 10) as OkObjectResult;
            var response = result?.Value as dynamic;

            
            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);

        }

        [Fact]
        public async Task GetById_ReturnsClient()
        {
            
            var clientId = Guid.NewGuid();
            var client = new Client { Id = clientId, FirstName = "Иван", LastName = "Иванов", DateOfBirth = new DateOnly(1990, 1, 1) };
            _clientRepositoryMock
                .Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync(client);

            
            var result = await _controller.GetById(clientId) as OkObjectResult;
            var response = result?.Value as Client;

            
            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);
            Assert.Equal(clientId, response?.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfClientDoesNotExist()
        {
            
            var clientId = Guid.NewGuid();
            _clientRepositoryMock
                .Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync((Client?)null);

            
            var result = await _controller.GetById(clientId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
                       var client = new Client { Id = Guid.NewGuid(), FirstName = "Иван", LastName = "Иванов", DateOfBirth = new DateOnly(1990, 1, 1) };
            _clientRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Client>()))
                .Returns(Task.CompletedTask);

            
            var result = await _controller.Create(client) as CreatedAtActionResult;

            
            Assert.NotNull(result);
            Assert.Equal(201, result?.StatusCode);
            Assert.Equal("GetById", result?.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            
            var clientId = Guid.NewGuid();
            var client = new Client { Id = clientId, FirstName = "Иван", LastName = "Иванов", DateOfBirth = new DateOnly(1990, 1, 1) };
            _clientRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Client>()))
                .Returns(Task.CompletedTask);

            
            var result = await _controller.Update(clientId, client);

            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            
            var clientId = Guid.NewGuid();
            var client = new Client { Id = Guid.NewGuid(), FirstName = "Иван", LastName = "Иванов", DateOfBirth = new DateOnly(1990, 1, 1) };

            
            var result = await _controller.Update(clientId, client);

            
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
           
            var clientId = Guid.NewGuid();
            var client = new Client { Id = clientId, FirstName = "Иван", LastName = "Иванов" };

            
            _clientRepositoryMock
                .Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync(client);

            
            _clientRepositoryMock
                .Setup(repo => repo.DeleteAsync(clientId))
                .Returns(Task.CompletedTask);
            
            var result = await _controller.Delete(clientId);

            
            Assert.IsType<NoContentResult>(result);  // Ожидаем NoContentResult
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenClientDoesNotExist()
        {
            
            var clientId = Guid.NewGuid();
            
            _clientRepositoryMock
                .Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync((Client?)null);

            var result = await _controller.Delete(clientId);

            
            Assert.IsType<NotFoundResult>(result);  // Ожидаем NotFoundResult
        }

    }
}
