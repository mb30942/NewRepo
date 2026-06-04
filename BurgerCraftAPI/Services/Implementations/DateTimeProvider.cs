using BurgerCraftAPI.Services.Interfaces;

namespace BurgerCraftAPI.Services.Implementations
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
