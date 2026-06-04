using BurgerCraftAPI.Models;

namespace BurgerCraftAPI.Repositories.Interfaces
{
    public interface IMyOrderRepository
    {
        Task AddMyOrder(MyOrder myOrder);
        Task<IEnumerable<MyOrder>> GetAll();
        Task<IEnumerable<MyOrder>> GetAllByUserId(string userId);
        Task Delete(int id);
    }
}
