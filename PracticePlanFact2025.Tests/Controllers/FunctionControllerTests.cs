using Data.Models.Functions;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace PracticePlanFact2025.Tests.Controllers
{
    public class FunctionsControllerTests
    {
        private readonly Mock<IFunctionRepository> _functionRepositoryMock;
        private readonly FunctionsController _controller;

        public FunctionsControllerTests()
        {
            _functionRepositoryMock = new Mock<IFunctionRepository>();
            _controller = new FunctionsController(_functionRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBirthdayOrdersTotal_ReturnsOkResult_WithList()
        {
            var expectedList = new List<BirthdayOrderTotal>
            {
                new BirthdayOrderTotal { Total = 1500.75m }
            };

            _functionRepositoryMock
                .Setup(repo => repo.GetBirthdayOrdersTotalAsync())
                .ReturnsAsync(expectedList);

            var result = await _controller.GetBirthdayOrdersTotal() as OkObjectResult;
            var actualList = result?.Value as IEnumerable<BirthdayOrderTotal>;

            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);
            Assert.NotNull(actualList);
            Assert.Single(actualList!);
        }

        [Fact]
        public async Task GetAvgOrderSumPerHour_ReturnsOkResult_WithList()
        {
            var expectedList = new List<AvgOrderSumPerHour>
            {
                new AvgOrderSumPerHour { Hour = 10, AvgSum = 100.50m },
                new AvgOrderSumPerHour { Hour = 15, AvgSum = 250.00m }
            };

            _functionRepositoryMock
                .Setup(repo => repo.GetAvgOrderSumPerHourAsync())
                .ReturnsAsync(expectedList);

            var result = await _controller.GetAvgOrderSumPerHour() as OkObjectResult;
            var actualList = result?.Value as IEnumerable<AvgOrderSumPerHour>;

            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);
            Assert.NotNull(actualList);
            Assert.Equal(2, actualList!.Count());
        }
    }
}
