using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
