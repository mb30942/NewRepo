using BurgerCraftAPI.Services.Interfaces;

namespace BurgerCraftAPI.Services.Implementations
{
    public class TimeSensitiveOfferService : ITimeSensitiveOfferService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public TimeSensitiveOfferService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public bool IsTimeSensitiveOfferActive()
        {
            var now = _dateTimeProvider.Now;
            return now.Hour >= 20 && now.Hour < 23;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return IsTimeSensitiveOfferActive() ? originalPrice * 0.8m : originalPrice;
        }
    }
}
