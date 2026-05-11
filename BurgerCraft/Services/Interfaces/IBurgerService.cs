using BurgerCraft.Models;

namespace BurgerCraft.Services.Interfaces
{
    public interface IBurgerService
    {
        Task<IEnumerable<Burger>> GetAllBurgers();
        Task<Burger> GetBurgerById(int id);
        Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId);
        Task AddBurger(Burger burger);
        Task Delete(int id);
        Task UpdateBurger(Burger burger);
    }
}
