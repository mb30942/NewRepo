using BurgerCraft.Models;

namespace BurgerCraft.Repositories.Interfaces
{
    public interface IMyOrderRepository
    {
        Task AddMyOrder(MyOrder myOrder);
        Task<IEnumerable<MyOrder>> GetAll();
    }
}
