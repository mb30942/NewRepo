using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Services.Interfaces
{
    public interface IBurgerTypeService
    {
        Task<IEnumerable<BurgerType>> GetAll();
        Task<BurgerType?> GetById(int id);
        Task Add(BurgerType burgerType);
        Task Delete(int id);
    }
}
