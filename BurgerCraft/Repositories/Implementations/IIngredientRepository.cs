using BurgerCraft.Models;

namespace BurgerCraft.Repositories.Implementations
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllIngredients();
        Task<Ingredient> GetIngredientById(int id);
        Task AddIngredient(Ingredient ingredient);
        Task UpdateIngredient(Ingredient ingredient);
        Task DeleteIngredient(int id);
    }
}
