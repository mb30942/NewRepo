using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraft.Repositories.Interfaces
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
            return await _context.Burgers.Include(b => b.BurgerType).Include(b => b.BurgerIngredients).ThenInclude(bi => bi.Ingredient).ToListAsync();
        }

        public async Task<Burger> GetBurgerById(int id)
        {
            var burger = await _context.Burgers
            .Include(b => b.BurgerType)
            .Include(b => b.BurgerIngredients)
                .ThenInclude(bi => bi.Ingredient)
            .Where(b => b.Id == id)  
            .FirstOrDefaultAsync();
            
            return burger;
        }

        public async Task AddBurger(Burger burger)
        {
            _context.Burgers.Add(burger);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId)
        {
            return await _context.Burgers
                .Where(b => b.BurgerTypeId == burgerTypeId)
                .Include(b => b.BurgerType)
                .ToListAsync();
        }
        public async Task Delete(int id)
        {
            var deleteBurger =  _context.Burgers.Find(id);
            _context.Burgers.Remove(deleteBurger);
            await _context.SaveChangesAsync();  
        }

    }
}
