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
            return await _context.Burgers.Include(b => b.BurgerType).ToListAsync();
        }

        public async Task<Burger> GetBurgerById(int id)
        {
            return await _context.Burgers
                .Include(b => b.BurgerType)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Burger>> GetBurgersByType(int burgerTypeId)
        {
            return await _context.Burgers
                .Where(b => b.BurgerTypeId == burgerTypeId)
                .Include(b => b.BurgerType)
                .ToListAsync();
        }

    }
}
