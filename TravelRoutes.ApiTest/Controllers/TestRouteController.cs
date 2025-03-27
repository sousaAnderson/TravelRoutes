using Dapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using System.Data;
using TravelRoutes.Domain.Entities;
using TravelRoutes.Infrastructure.Repository;
using TravelRoutes.Domain.Abstractions;
using TravelRoutes.Application.Services;

namespace TravelRoutes.ApiTest.Controllers
{
    public class TestRouteController
    {
        private readonly Mock<IRouteRepository> _mockRepo;
        private readonly RouteService _routeService;

        public TestRouteController()
        {
            _mockRepo = new Mock<IRouteRepository>();
            _routeService = new RouteService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnListOfRoutes()
        {
            // Arrange
            var listRoutes = new List<Domain.Entities.Route>
            {
                new Domain.Entities.Route { Id = 1, Origin = "GYN", Destination = "BSB", Price = 125.99m },
                new Domain.Entities.Route { Id = 2, Origin = "GIG", Destination = "VCP", Price = 99.99m }
            };

            _mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(listRoutes);

            var result = await _routeService.GetAll();

            Assert.NotNull(result);
            Assert.Contains(result, p => p.Origin == "GYN");
        }

        [Fact]
        public async Task GetRoute_ReturnRoute()
        {
            // Arrange
            var routes = new Domain.Entities.Route { Id = 1, Origin = "THE", Destination = "BSB", Price = 320.99m };

            _mockRepo.Setup(repo => repo.GetRoute(1)).ReturnsAsync(routes);
            // Act
            var result = await _routeService.GetRoute(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(routes.Id, result.Id);
        }

        [Fact]
        public async Task PostAdd_ReturnIdOfInsertedRoute()
        {
            // Arrange
            var route = new Domain.Entities.Route { Origin = "GRU", Destination = "GYN", Price = 150.29m };
            _mockRepo.Setup(repo => repo.Add(route)).ReturnsAsync(1);

            // Act
            var result = await _routeService.Add(route);
           
            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Put_ReturnTrueSucess()
        {
            // Arrange
            var route = new Domain.Entities.Route { Id = 1, Origin = "THE", Destination = "BSB", Price = 510.00m };

            _mockRepo.Setup(repo => repo.Update(route)).ReturnsAsync(true);

            // Act
            var result = await _routeService.Update(route);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ReturnTrueSucess()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Delete(1)).ReturnsAsync(true);

            // Act
            var result = await _routeService.Delete(1);

            // Assert
            Assert.True(result);
        }

    }
}
