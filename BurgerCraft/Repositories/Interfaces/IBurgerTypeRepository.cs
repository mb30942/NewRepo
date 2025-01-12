using BurgerCraft.Models;

namespace BurgerCraft.Repositories.Implementations
{
    public interface IBurgerTypeRepository
    {
        IEnumerable<BurgerType> GetAll();
        BurgerType GetById(int id);

        public void Add(BurgerType burgerType);
        public void Delete(int id);
    }
}
