using BurgerCraft.Models;

namespace BurgerCraft.Services.Interfaces
{
    public interface IMyOrderService
    {
        Task AddMyOrder(MyOrder myOrder);
        Task<IEnumerable<MyOrder>> GetAll();
        Task Delete(int id);
        Task<IEnumerable<MyOrder>> GetAllByUserId(string userId);
    }
}
