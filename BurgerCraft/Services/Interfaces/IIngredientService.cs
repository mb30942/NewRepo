using BurgerCraft.Models;

namespace BurgerCraft.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAllIngredients();
        Task AddIngredient(Ingredient ingredient);
        Task<Ingredient> GetIngredientById(int id);
        Task UpdateIngredient(Ingredient ingredient);
        Task DeleteIngredient(int id);
    }
}
