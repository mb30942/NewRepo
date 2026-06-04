using BurgerCraftAPI.Services.Implementations;
using BurgerCraftAPI.Services.Interfaces;
using Moq;
using Xunit;

namespace BurgerCraftAPI.Tests.Services
{
    public class TimeSensitiveOfferServiceTests
    {
        private TimeSensitiveOfferService CreateService(DateTime now)
        {
            var mockProvider = new Mock<IDateTimeProvider>();
            mockProvider.Setup(p => p.Now).Returns(now);
            return new TimeSensitiveOfferService(mockProvider.Object);
        }

        [Theory]
        [InlineData(20, 0)]
        [InlineData(21, 30)]
        [InlineData(22, 59)]
        public void IsTimeSensitiveOfferActive_ReturnsTrue_DuringOfferHours(int hour, int minute)
        {
            var service = CreateService(new DateTime(2024, 1, 1, hour, minute, 0));
            Assert.True(service.IsTimeSensitiveOfferActive());
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(19, 59)]
        [InlineData(23, 0)]
        [InlineData(12, 0)]
        public void IsTimeSensitiveOfferActive_ReturnsFalse_OutsideOfferHours(int hour, int minute)
        {
            var service = CreateService(new DateTime(2024, 1, 1, hour, minute, 0));
            Assert.False(service.IsTimeSensitiveOfferActive());
        }

        [Fact]
        public void ApplyDiscount_ReturnsDiscountedPrice_WhenOfferIsActive()
        {
            var service = CreateService(new DateTime(2024, 1, 1, 21, 0, 0));
            var result = service.ApplyDiscount(10.00m);
            Assert.Equal(8.00m, result);
        }

        [Fact]
        public void ApplyDiscount_ReturnsOriginalPrice_WhenOfferIsNotActive()
        {
            var service = CreateService(new DateTime(2024, 1, 1, 12, 0, 0));
            var result = service.ApplyDiscount(10.00m);
            Assert.Equal(10.00m, result);
        }

        [Fact]
        public void ApplyDiscount_AppliesTwentyPercentOff()
        {
            var service = CreateService(new DateTime(2024, 1, 1, 20, 0, 0));
            var result = service.ApplyDiscount(25.00m);
            Assert.Equal(20.00m, result);
        }
    }
}
