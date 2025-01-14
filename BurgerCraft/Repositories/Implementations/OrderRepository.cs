using BurgerCraft.Models;
using BurgerCraft.Repositories.Interfaces;

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

    }
}
