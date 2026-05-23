using BurgerCraft.Models;

namespace BurgerCraft.Services.Interfaces
{
    public interface IMyOrderService
    {
        Task AddMyOrder(MyOrder myOrder);
        Task<IEnumerable<MyOrder>> GetAll();
        Task Delete(int id);
        Task<IEnumerable<MyOrder>> GetAllByUserId(string userId);
        Task<MyOrder> PrepareOrderAsync(string userId, int burgerId, int quantity, List<int> selectedIngredients);
        Task<(decimal TotalPrice, int OrderNumber)> SecureOrderAsync(string userId);
    }
}
