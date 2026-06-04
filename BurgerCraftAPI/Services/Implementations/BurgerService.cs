using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using BurgerCraftAPI.Services.Interfaces;

namespace BurgerCraftAPI.Services.Implementations
{
    public class BurgerService : IBurgerService
    {
        private readonly IBurgerRepository _burgerRepository;
        private readonly IImageService _imageService;

        public BurgerService(IBurgerRepository burgerRepository, IImageService imageService)
        {
            _burgerRepository = burgerRepository;
            _imageService = imageService;
        }

        public async Task<IEnumerable<Burger>> GetAllBurgers()
        {
            return await _burgerRepository.GetAllBurgers();
        }

        public async Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId)
        {
            return await _burgerRepository.GetBurgersByType(burgerTypeId);
        }

        public async Task<Burger?> GetBurgerById(int id)
        {
            return await _burgerRepository.GetBurgerById(id);
        }

        public async Task AddBurger(Burger burger, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
                burger.ImagePath = await _imageService.SaveImageAsync(imageFile);

            await _burgerRepository.AddBurger(burger);
        }

        public async Task UpdateBurger(Burger burger, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
                burger.ImagePath = await _imageService.SaveImageAsync(imageFile);

            await _burgerRepository.UpdateBurger(burger);
        }

        public async Task Delete(int id)
        {
            await _burgerRepository.Delete(id);
        }
    }
}
