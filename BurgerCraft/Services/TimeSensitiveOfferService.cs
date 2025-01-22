
namespace BurgerCraft.Services
{
    public class TimeSensitiveOfferService
    {
        public bool IsTimeSensitiveOfferActive()
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var startTime = new TimeSpan(20, 0, 0); 
            var endTime = new TimeSpan(23, 0, 0);   

            return currentTime >= startTime && currentTime <= endTime;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            if (IsTimeSensitiveOfferActive())
            {
                return originalPrice * 0.8m; 
            }
            return originalPrice; 
        }
    }
}