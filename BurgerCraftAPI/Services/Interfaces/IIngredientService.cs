using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAllIngredients();
        Task<Ingredient?> GetIngredientById(int id);
        Task AddIngredient(Ingredient ingredient);
        Task UpdateIngredient(Ingredient ingredient);
        Task DeleteIngredient(int id);
    }
}
