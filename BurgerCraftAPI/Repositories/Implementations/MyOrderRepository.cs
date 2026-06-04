using BurgerCraftAPI.Data;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraftAPI.Repositories.Implementations
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
            _context.MyOrders.Add(myOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyOrder>> GetAll()
        {
            return await _context.MyOrders.ToListAsync();
        }

        public async Task<IEnumerable<MyOrder>> GetAllByUserId(string userId)
        {
            return await _context.MyOrders
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        public async Task Delete(int id)
        {
            var myOrder = await _context.MyOrders.FindAsync(id);
            if (myOrder != null)
            {
                _context.MyOrders.Remove(myOrder);
                await _context.SaveChangesAsync();
            }
        }
    }
}
