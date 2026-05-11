using BurgerCraft.Models;

namespace BurgerCraft.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
