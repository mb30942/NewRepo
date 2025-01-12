using BurgerCraft.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerCraft.Repositories.Interfaces;

namespace BurgerCraft.Repositories.Implementations
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

        public async Task<Ingredient> GetIngredientById(int id)
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
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredient(int id)
        {
            var ingredient = await GetIngredientById(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
