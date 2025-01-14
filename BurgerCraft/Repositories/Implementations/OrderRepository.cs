using BurgerCraft.Models;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraft.Repositories.Implementations
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
            Console.WriteLine(order);
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync(); 
        }

    }
}
