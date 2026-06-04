using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using BurgerCraftAPI.Services.Implementations;
using Moq;
using Xunit;

namespace BurgerCraftAPI.Tests.Services
{
    public class IngredientServiceTests
    {
        private readonly Mock<IIngredientRepository> _repoMock = new();

        private IngredientService CreateService() =>
            new IngredientService(_repoMock.Object);

        [Fact]
        public async Task GetAllIngredients_ReturnsAllIngredients()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Lettuce", Price = 0.50m },
                new Ingredient { Id = 2, Name = "Tomato", Price = 0.75m }
            };
            _repoMock.Setup(r => r.GetAllIngredients()).ReturnsAsync(ingredients);

            var result = await CreateService().GetAllIngredients();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetIngredientById_ReturnsCorrectIngredient()
        {
            var ingredient = new Ingredient { Id = 1, Name = "Lettuce", Price = 0.50m };
            _repoMock.Setup(r => r.GetIngredientById(1)).ReturnsAsync(ingredient);

            var result = await CreateService().GetIngredientById(1);

            Assert.NotNull(result);
            Assert.Equal("Lettuce", result.Name);
            Assert.Equal(0.50m, result.Price);
        }

        [Fact]
        public async Task GetIngredientById_ReturnsNull_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetIngredientById(999)).ReturnsAsync((Ingredient?)null);

            var result = await CreateService().GetIngredientById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddIngredient_CallsRepositoryWithIngredient()
        {
            var ingredient = new Ingredient { Name = "Bacon", Price = 2.00m };
            _repoMock.Setup(r => r.AddIngredient(ingredient)).Returns(Task.CompletedTask);

            await CreateService().AddIngredient(ingredient);

            _repoMock.Verify(r => r.AddIngredient(ingredient), Times.Once);
        }

        [Fact]
        public async Task UpdateIngredient_CallsRepositoryWithUpdatedIngredient()
        {
            var ingredient = new Ingredient { Id = 1, Name = "Bacon Updated", Price = 2.50m };
            _repoMock.Setup(r => r.UpdateIngredient(ingredient)).Returns(Task.CompletedTask);

            await CreateService().UpdateIngredient(ingredient);

            _repoMock.Verify(r => r.UpdateIngredient(ingredient), Times.Once);
        }

        [Fact]
        public async Task DeleteIngredient_CallsRepositoryWithId()
        {
            _repoMock.Setup(r => r.DeleteIngredient(1)).Returns(Task.CompletedTask);

            await CreateService().DeleteIngredient(1);

            _repoMock.Verify(r => r.DeleteIngredient(1), Times.Once);
        }
    }
}
