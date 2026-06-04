namespace BurgerCraftAPI.Services.Interfaces
{
    public interface ITimeSensitiveOfferService
    {
        bool IsTimeSensitiveOfferActive();
        decimal ApplyDiscount(decimal originalPrice);
    }
}
