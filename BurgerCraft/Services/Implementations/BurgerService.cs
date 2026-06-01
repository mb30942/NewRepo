using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Services.Interfaces;

namespace BurgerCraft.Services.Implementations
{
    public class BurgerService : IBurgerService
    {
        private readonly IBurgerRepository _burgerRepository;
        private readonly ITimeSensitiveOfferService _offerService;
        private readonly IImageService _imageService;

        public BurgerService(IBurgerRepository burgerRepository, ITimeSensitiveOfferService offerService, IImageService imageService)
        {
            _burgerRepository = burgerRepository;
            _offerService = offerService;
            _imageService = imageService;
        }

        public async Task<IEnumerable<Burger>> GetAllBurgers()
        {
            return await _burgerRepository.GetAllBurgers();
        }

        public async Task<IEnumerable<Burger>> GetAllBurgersWithDiscount()
        {
            var burgers = await _burgerRepository.GetAllBurgers();
            foreach (var burger in burgers)
            {
                burger.Price = _offerService.ApplyDiscount(burger.Price);
            }
            return burgers;
        }

        public async Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId)
        {
            return await _burgerRepository.GetBurgersByType(burgerTypeId);
        }

        public async Task<IEnumerable<Burger>> GetBurgersByTypeWithDiscount(int burgerTypeId)
        {
            var burgers = await _burgerRepository.GetBurgersByType(burgerTypeId);
            foreach (var burger in burgers)
            {
                burger.Price = _offerService.ApplyDiscount(burger.Price);
            }
            return burgers;
        }

        public async Task<Burger> GetBurgerById(int id)
        {
            return await _burgerRepository.GetBurgerById(id);
        }

        public async Task<Burger> GetBurgerByIdWithDiscount(int id)
        {
            var burger = await _burgerRepository.GetBurgerById(id);
            if (burger != null)
            {
                burger.Price = _offerService.ApplyDiscount(burger.Price);
            }
            return burger;
        }

        public async Task AddBurger(Burger burger, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                burger.ImagePath = await _imageService.SaveImageAsync(imageFile);
            }
            await _burgerRepository.AddBurger(burger);
        }

        public async Task UpdateBurger(Burger burger, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                burger.ImagePath = await _imageService.SaveImageAsync(imageFile);
            }
            await _burgerRepository.UpdateBurger(burger);
        }

        public async Task Delete(int id)
        {
            await _burgerRepository.Delete(id);
        }
    }
}