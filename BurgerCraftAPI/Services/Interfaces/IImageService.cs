namespace BurgerCraftAPI.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
