using BurgerCraftAPI.Data;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraftAPI.Repositories.Implementations
{
    public class BurgerTypeRepository : IBurgerTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public BurgerTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BurgerType>> GetAll()
        {
            return await _context.BurgerTypes.ToListAsync();
        }

        public async Task<BurgerType?> GetById(int id)
        {
            return await _context.BurgerTypes.FindAsync(id);
        }

        public async Task Add(BurgerType burgerType)
        {
            _context.BurgerTypes.Add(burgerType);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var burgerType = await _context.BurgerTypes.FindAsync(id);
            if (burgerType != null)
            {
                _context.BurgerTypes.Remove(burgerType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
