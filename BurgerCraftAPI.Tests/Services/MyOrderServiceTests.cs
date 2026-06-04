using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using BurgerCraftAPI.Services.Implementations;
using BurgerCraftAPI.Services.Interfaces;
using Moq;
using Xunit;

namespace BurgerCraftAPI.Tests.Services
{
    public class MyOrderServiceTests
    {
        private readonly Mock<IMyOrderRepository> _myOrderRepoMock = new();
        private readonly Mock<IBurgerService> _burgerServiceMock = new();
        private readonly Mock<IIngredientService> _ingredientServiceMock = new();
        private readonly Mock<ITimeSensitiveOfferService> _offerServiceMock = new();
        private readonly Mock<IOrderService> _orderServiceMock = new();

        private MyOrderService CreateService() => new MyOrderService(
            _myOrderRepoMock.Object,
            _burgerServiceMock.Object,
            _ingredientServiceMock.Object,
            _offerServiceMock.Object,
            _orderServiceMock.Object);

        [Fact]
        public async Task PrepareOrderAsync_ThrowsKeyNotFoundException_WhenBurgerNotFound()
        {
            _burgerServiceMock.Setup(s => s.GetBurgerById(999)).ReturnsAsync((Burger?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                CreateService().PrepareOrderAsync("user1", 999, 1, new List<int>()));
        }

        [Fact]
        public async Task PrepareOrderAsync_CalculatesTotalPrice_WithSelectedIngredients()
        {
            var burger = new Burger
            {
                Id = 1,
                Name = "Classic Beef",
                Price = 7.99m,
                BurgerIngredients = new List<BurgerIngredient>()
            };
            var ingredient = new Ingredient { Id = 4, Name = "Beef Patty", Price = 3.00m };

            _burgerServiceMock.Setup(s => s.GetBurgerById(1)).ReturnsAsync(burger);
            _offerServiceMock.Setup(s => s.ApplyDiscount(7.99m)).Returns(7.99m);
            _ingredientServiceMock.Setup(s => s.GetIngredientById(4)).ReturnsAsync(ingredient);

            var result = await CreateService().PrepareOrderAsync("user1", 1, 1, new List<int> { 4 });

            Assert.Equal("user1", result.UserId);
            Assert.Equal(1, result.BurgerId);
            Assert.Equal(10.99m, result.TotalPrice);
            Assert.Contains(4, result.IngredientIds);
        }

        [Fact]
        public async Task PrepareOrderAsync_UsesDefaultIngredients_WhenNoneSelected()
        {
            var burger = new Burger
            {
                Id = 1,
                Price = 5.99m,
                BurgerIngredients = new List<BurgerIngredient>
                {
                    new BurgerIngredient { IngredientId = 1 },
                    new BurgerIngredient { IngredientId = 2 }
                }
            };
            _burgerServiceMock.Setup(s => s.GetBurgerById(1)).ReturnsAsync(burger);
            _offerServiceMock.Setup(s => s.ApplyDiscount(5.99m)).Returns(5.99m);
            _ingredientServiceMock.Setup(s => s.GetIngredientById(It.IsAny<int>())).ReturnsAsync((Ingredient?)null);

            var result = await CreateService().PrepareOrderAsync("user1", 1, 1, new List<int>());

            Assert.Equal(new List<int> { 1, 2 }, result.IngredientIds);
        }

        [Fact]
        public async Task PrepareOrderAsync_AppliesDiscountToPrice_WhenOfferIsActive()
        {
            var burger = new Burger { Id = 1, Price = 10.00m, BurgerIngredients = new List<BurgerIngredient>() };
            _burgerServiceMock.Setup(s => s.GetBurgerById(1)).ReturnsAsync(burger);
            _offerServiceMock.Setup(s => s.ApplyDiscount(10.00m)).Returns(8.00m);

            var result = await CreateService().PrepareOrderAsync("user1", 1, 1, new List<int>());

            Assert.Equal(8.00m, result.TotalPrice);
        }

        [Fact]
        public async Task PrepareOrderAsync_MultipliesQuantity()
        {
            var burger = new Burger { Id = 1, Price = 5.00m, BurgerIngredients = new List<BurgerIngredient>() };
            _burgerServiceMock.Setup(s => s.GetBurgerById(1)).ReturnsAsync(burger);
            _offerServiceMock.Setup(s => s.ApplyDiscount(5.00m)).Returns(5.00m);

            var result = await CreateService().PrepareOrderAsync("user1", 1, 3, new List<int>());

            Assert.Equal(15.00m, result.TotalPrice);
        }

        [Fact]
        public async Task SecureOrderAsync_ConvertsCartToOrdersAndClearsCart()
        {
            var myOrders = new List<MyOrder>
            {
                new MyOrder { Id = 1, UserId = "user1", BurgerId = 1, IngredientIds = new List<int> { 1 }, TotalPrice = 7.99m },
                new MyOrder { Id = 2, UserId = "user1", BurgerId = 2, IngredientIds = new List<int> { 2 }, TotalPrice = 5.99m }
            };

            _myOrderRepoMock.Setup(r => r.GetAllByUserId("user1")).ReturnsAsync(myOrders);
            _orderServiceMock.Setup(s => s.AddOrder(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _myOrderRepoMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            var (totalPrice, orderNumber) = await CreateService().SecureOrderAsync("user1");

            Assert.Equal(13.98m, totalPrice);
            Assert.InRange(orderNumber, 10000, 99999);
            _orderServiceMock.Verify(s => s.AddOrder(It.IsAny<Order>()), Times.Exactly(2));
            _myOrderRepoMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public async Task GetEnrichedOrdersByUserId_ReturnsOrdersWithBurgerAndIngredientNames()
        {
            var orders = new List<MyOrder>
            {
                new MyOrder { Id = 1, BurgerId = 1, IngredientIds = new List<int> { 1, 2 }, TotalPrice = 8.99m }
            };
            var burgers = new List<Burger> { new Burger { Id = 1, Name = "Classic Beef", Price = 7.99m, BurgerIngredients = new List<BurgerIngredient>() } };
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Lettuce", Price = 0.50m },
                new Ingredient { Id = 2, Name = "Tomato", Price = 0.75m }
            };

            _myOrderRepoMock.Setup(r => r.GetAllByUserId("user1")).ReturnsAsync(orders);
            _burgerServiceMock.Setup(s => s.GetAllBurgers()).ReturnsAsync(burgers);
            _ingredientServiceMock.Setup(s => s.GetAllIngredients()).ReturnsAsync(ingredients);

            var result = (await CreateService().GetEnrichedOrdersByUserId("user1")).ToList();

            Assert.Single(result);
            Assert.Equal("Classic Beef", result[0].BurgerName);
            Assert.Equal(new List<string> { "Lettuce", "Tomato" }, result[0].Ingredients);
        }
    }
}
