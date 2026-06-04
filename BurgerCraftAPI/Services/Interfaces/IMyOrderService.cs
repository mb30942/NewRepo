using BurgerCraftAPI.DTOs.MyOrders;
using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Services.Interfaces
{
    public interface IMyOrderService
    {
        Task AddMyOrder(MyOrder myOrder);
        Task<IEnumerable<MyOrder>> GetAll();
        Task<IEnumerable<MyOrder>> GetAllByUserId(string userId);
        Task Delete(int id);
        Task<IEnumerable<MyOrderDto>> GetEnrichedOrdersByUserId(string userId);
        Task<MyOrder> PrepareOrderAsync(string userId, int burgerId, int quantity, List<int> selectedIngredients);
        Task<(decimal TotalPrice, int OrderNumber)> SecureOrderAsync(string userId);
    }
}
