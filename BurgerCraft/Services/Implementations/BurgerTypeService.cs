using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Services.Interfaces;

namespace BurgerCraft.Services.Implementations
{
    public class BurgerTypeService : IBurgerTypeService
    {
        private readonly IBurgerTypeRepository _burgerTypeRepository;

        public BurgerTypeService(IBurgerTypeRepository burgerTypeRepository)
        {
            _burgerTypeRepository = burgerTypeRepository;
        }

        public IEnumerable<BurgerType> GetAll()
        {
            return _burgerTypeRepository.GetAll();
        }

        public BurgerType GetById(int id)
        {
            return _burgerTypeRepository.GetById(id);
        }

        public void Add(BurgerType entity)
        {
            _burgerTypeRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _burgerTypeRepository.Delete(id);
        }
    }
}
