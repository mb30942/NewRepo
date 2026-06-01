using BurgerCraft.Models;
using Microsoft.AspNetCore.Http;

namespace BurgerCraft.Services.Interfaces
{
    public interface IBurgerService
    {
        Task<IEnumerable<Burger>> GetAllBurgers();
        Task<IEnumerable<Burger>> GetAllBurgersWithDiscount();
        Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId);
        Task<IEnumerable<Burger>> GetBurgersByTypeWithDiscount(int burgerTypeId);
        Task<Burger> GetBurgerById(int id);
        Task<Burger> GetBurgerByIdWithDiscount(int id);
        Task AddBurger(Burger burger, IFormFile imageFile);
        Task UpdateBurger(Burger burger, IFormFile imageFile);
        Task Delete(int id);
    }
}