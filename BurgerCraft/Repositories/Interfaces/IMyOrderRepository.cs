using BurgerCraft.Models;

namespace BurgerCraft.Repositories.Interfaces
{
    public interface IMyOrderRepository
    {
        Task AddMyOrder(MyOrder myOrder);
        Task<IEnumerable<MyOrder>> GetAll();
        Task Delete(int id);
        Task<IEnumerable<MyOrder>> GetAllByUserId(string userId);
    }
}
