using BurgerCraftAPI.Data;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraftAPI.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
