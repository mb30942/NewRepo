using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraft.Repositories.Interfaces
{
    public class BurgerTypeRepository : IBurgerTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public BurgerTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(BurgerType burgerType)
        {
            _context.BurgerTypes.Add(burgerType);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var DeleteBurgerType = _context.BurgerTypes.Find(id);
            _context.BurgerTypes.Remove(DeleteBurgerType);
            _context.SaveChanges();
        }

        public IEnumerable<BurgerType> GetAll()
        {
            return _context.BurgerTypes.ToList();
        }

        public BurgerType GetById(int id)
        {
            return _context.BurgerTypes.Find(id);
        }
    }
}
