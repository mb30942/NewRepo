using BurgerCraft.Models;

namespace BurgerCraft.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
    }
}
