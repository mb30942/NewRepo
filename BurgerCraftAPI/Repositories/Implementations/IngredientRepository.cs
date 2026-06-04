using BurgerCraftAPI.Data;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraftAPI.Repositories.Implementations
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient?> GetIngredientById(int id)
        {
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task AddIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIngredient(Ingredient ingredient)
        {
            var existing = await _context.Ingredients.FindAsync(ingredient.Id);
            if (existing == null) return;

            existing.Name = ingredient.Name;
            existing.Price = ingredient.Price;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
