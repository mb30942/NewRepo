using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Services.Interfaces
{
    public interface IBurgerService
    {
        Task<IEnumerable<Burger>> GetAllBurgers();
        Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId);
        Task<Burger?> GetBurgerById(int id);
        Task AddBurger(Burger burger, IFormFile? imageFile);
        Task UpdateBurger(Burger burger, IFormFile? imageFile);
        Task Delete(int id);
    }
}
