using BurgerCraftAPI.Models;
using BurgerCraftAPI.Repositories.Interfaces;
using BurgerCraftAPI.Services.Interfaces;

namespace BurgerCraftAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task AddOrder(Order order)
        {
            await _repository.AddOrder(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _repository.GetAllOrders();
        }
    }
}
