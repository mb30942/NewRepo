using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using BurgerCraftAPI.Services.Interfaces;

namespace BurgerCraftAPI.Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _repository;

        public IngredientService(IIngredientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return await _repository.GetAllIngredients();
        }

        public async Task<Ingredient?> GetIngredientById(int id)
        {
            return await _repository.GetIngredientById(id);
        }

        public async Task AddIngredient(Ingredient ingredient)
        {
            await _repository.AddIngredient(ingredient);
        }

        public async Task UpdateIngredient(Ingredient ingredient)
        {
            await _repository.UpdateIngredient(ingredient);
        }

        public async Task DeleteIngredient(int id)
        {
            await _repository.DeleteIngredient(id);
        }
    }
}
