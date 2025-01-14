using BurgerCraft.Models;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraft.Repositories.Implementations
{
    public class MyOrderRepository : IMyOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public MyOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddMyOrder(MyOrder myOrder)
        {
            Console.WriteLine(myOrder);
            _context.MyOrders.Add(myOrder);
            _context.SaveChanges();
        }
        public async Task<IEnumerable<MyOrder>> GetAll()
        {
            return await _context.MyOrders.ToListAsync();
        }

        public async Task Delete(int id)
        {
            var deleteMyOrder = _context.MyOrders.Find(id);
            _context.MyOrders.Remove(deleteMyOrder);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<MyOrder>> GetAllByUserId(string userId)
        {
            return await _context.MyOrders
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

    }
}
