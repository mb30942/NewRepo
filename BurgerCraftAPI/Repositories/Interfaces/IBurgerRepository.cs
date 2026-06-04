using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Repositories.Interfaces
{
    public interface IBurgerRepository
    {
        Task<IEnumerable<Burger>> GetAllBurgers();
        Task<Burger?> GetBurgerById(int id);
        Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId);
        Task AddBurger(Burger burger);
        Task UpdateBurger(Burger burger);
        Task Delete(int id);
    }
}
