using BurgerCraft.Models;

namespace BurgerCraft.Repositories.Implementations
{
    public interface IBurgerRepository
    {
        Task<IEnumerable<Burger>> GetAllBurgers();
        Task<Burger> GetBurgerById(int id);
        Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId);
        Task AddBurger(Burger burger);
    }
}
