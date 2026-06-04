using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
