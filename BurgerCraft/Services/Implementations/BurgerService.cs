using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Services.Interfaces;

namespace BurgerCraft.Services.Implementations
{
    public class BurgerService : IBurgerService
    {
        private readonly IBurgerRepository _burgerRepository;

        public BurgerService(IBurgerRepository burgerRepository)
        {
            _burgerRepository = burgerRepository;
        }

        public async Task<IEnumerable<Burger>> GetAllBurgers()
        {
            return await _burgerRepository.GetAllBurgers();
        }

        public async Task<Burger> GetBurgerById(int id)
        {
            return await _burgerRepository.GetBurgerById(id);
        }

        public async Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId)
        {
            return await _burgerRepository.GetBurgersByType(burgerTypeId);
        }

        public async Task AddBurger(Burger burger)
        {
            await _burgerRepository.AddBurger(burger);
        }

        public async Task Delete(int id)
        {
            await _burgerRepository.Delete(id);
        }

        public async Task UpdateBurger(Burger burger)
        {
            await _burgerRepository.UpdateBurger(burger);
        }
    }
}
