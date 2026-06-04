using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using BurgerCraftAPI.Services.Implementations;
using BurgerCraftAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace BurgerCraftAPI.Tests.Services
{
    public class BurgerServiceTests
    {
        private readonly Mock<IBurgerRepository> _repoMock = new();
        private readonly Mock<IImageService> _imageMock = new();

        private BurgerService CreateService() =>
            new BurgerService(_repoMock.Object, _imageMock.Object);

        [Fact]
        public async Task GetAllBurgers_ReturnsBurgersFromRepository()
        {
            var burgers = new List<Burger>
            {
                new Burger { Id = 1, Name = "Classic Beef", Price = 7.99m, BurgerTypeId = 3 },
                new Burger { Id = 2, Name = "Veggie Delight", Price = 5.99m, BurgerTypeId = 1 }
            };
            _repoMock.Setup(r => r.GetAllBurgers()).ReturnsAsync(burgers);

            var result = await CreateService().GetAllBurgers();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetBurgerById_ReturnsCorrectBurger()
        {
            var burger = new Burger { Id = 1, Name = "Classic Beef", Price = 7.99m, BurgerTypeId = 3 };
            _repoMock.Setup(r => r.GetBurgerById(1)).ReturnsAsync(burger);

            var result = await CreateService().GetBurgerById(1);

            Assert.NotNull(result);
            Assert.Equal("Classic Beef", result.Name);
        }

        [Fact]
        public async Task GetBurgerById_ReturnsNull_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetBurgerById(999)).ReturnsAsync((Burger?)null);

            var result = await CreateService().GetBurgerById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddBurger_SavesImagePath_WhenImageProvided()
        {
            var burger = new Burger { Name = "Test", Price = 5.00m, BurgerTypeId = 1 };
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.Length).Returns(100L);
            _imageMock.Setup(s => s.SaveImageAsync(mockFile.Object)).ReturnsAsync("/images/test.jpg");
            _repoMock.Setup(r => r.AddBurger(It.IsAny<Burger>())).Returns(Task.CompletedTask);

            await CreateService().AddBurger(burger, mockFile.Object);

            Assert.Equal("/images/test.jpg", burger.ImagePath);
            _repoMock.Verify(r => r.AddBurger(burger), Times.Once);
        }

        [Fact]
        public async Task AddBurger_DoesNotSaveImage_WhenImageIsNull()
        {
            var burger = new Burger { Name = "Test", Price = 5.00m, BurgerTypeId = 1, ImagePath = "/existing.jpg" };
            _repoMock.Setup(r => r.AddBurger(It.IsAny<Burger>())).Returns(Task.CompletedTask);

            await CreateService().AddBurger(burger, null);

            _imageMock.Verify(s => s.SaveImageAsync(It.IsAny<IFormFile>()), Times.Never);
            Assert.Equal("/existing.jpg", burger.ImagePath);
        }

        [Fact]
        public async Task Delete_CallsRepository()
        {
            _repoMock.Setup(r => r.Delete(1)).Returns(Task.CompletedTask);

            await CreateService().Delete(1);

            _repoMock.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public async Task GetBurgersByType_ReturnsFilteredBurgers()
        {
            var burgers = new List<Burger>
            {
                new Burger { Id = 1, Name = "Classic Beef", BurgerTypeId = 3 }
            };
            _repoMock.Setup(r => r.GetBurgersByType(3)).ReturnsAsync(burgers);

            var result = await CreateService().GetBurgersByType(3);

            Assert.Single(result);
            Assert.Equal(3, result.First().BurgerTypeId);
        }
    }
}
