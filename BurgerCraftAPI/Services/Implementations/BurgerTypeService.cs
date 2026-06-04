using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using BurgerCraftAPI.Services.Interfaces;

namespace BurgerCraftAPI.Services.Implementations
{
    public class BurgerTypeService : IBurgerTypeService
    {
        private readonly IBurgerTypeRepository _repository;

        public BurgerTypeService(IBurgerTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BurgerType>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<BurgerType?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(BurgerType burgerType)
        {
            await _repository.Add(burgerType);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
