using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Repositories.Interfaces
{
    public interface IBurgerTypeRepository
    {
        Task<IEnumerable<BurgerType>> GetAll();
        Task<BurgerType?> GetById(int id);
        Task Add(BurgerType burgerType);
        Task Delete(int id);
    }
}
