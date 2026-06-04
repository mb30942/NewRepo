using BurgerCraftAPI.Data;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraftAPI.Repositories.Implementations
{
    public class BurgerRepository : IBurgerRepository
    {
        private readonly ApplicationDbContext _context;

        public BurgerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Burger>> GetAllBurgers()
        {
            return await _context.Burgers
                .Include(b => b.BurgerType)
                .Include(b => b.BurgerIngredients)
                    .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();
        }

        public async Task<Burger?> GetBurgerById(int id)
        {
            return await _context.Burgers
                .Include(b => b.BurgerType)
                .Include(b => b.BurgerIngredients)
                    .ThenInclude(bi => bi.Ingredient)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId)
        {
            return await _context.Burgers
                .Where(b => b.BurgerTypeId == burgerTypeId)
                .Include(b => b.BurgerType)
                .Include(b => b.BurgerIngredients)
                    .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();
        }

        public async Task AddBurger(Burger burger)
        {
            _context.Burgers.Add(burger);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBurger(Burger burger)
        {
            var existing = await _context.Burgers
                .Include(b => b.BurgerIngredients)
                .FirstOrDefaultAsync(b => b.Id == burger.Id);

            if (existing == null) return;

            existing.Name = burger.Name;
            existing.Price = burger.Price;
            existing.Description = burger.Description;
            existing.BurgerTypeId = burger.BurgerTypeId;
            existing.ImagePath = burger.ImagePath;

            _context.BurgerIngredients.RemoveRange(existing.BurgerIngredients);
            foreach (var bi in burger.BurgerIngredients)
            {
                existing.BurgerIngredients.Add(new BurgerIngredient { IngredientId = bi.IngredientId });
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var burger = await _context.Burgers.FindAsync(id);
            if (burger != null)
            {
                _context.Burgers.Remove(burger);
                await _context.SaveChangesAsync();
            }
        }
    }
}
