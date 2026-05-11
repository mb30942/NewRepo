using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Services.Interfaces;

namespace BurgerCraft.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return await _ingredientRepository.GetAllIngredients();
        }

        public async Task AddIngredient(Ingredient ingredient)
        {
            await _ingredientRepository.AddIngredient(ingredient);
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            return await _ingredientRepository.GetIngredientById(id);
        }

        public async Task UpdateIngredient(Ingredient ingredient)
        {
            await _ingredientRepository.UpdateIngredient(ingredient);
        }

        public async Task DeleteIngredient(int id)
        {
            await _ingredientRepository.DeleteIngredient(id);
        }
    }
}
