using BurgerCraft.Models;
using BurgerCraft.Repositories.Interfaces;
using BurgerCraft.Services.Interfaces;

namespace BurgerCraft.Services.Implementations
{
    public class MyOrderService : IMyOrderService
    {
        private readonly IMyOrderRepository _myOrderRepository;

        public MyOrderService(IMyOrderRepository myOrderRepository)
        {
            _myOrderRepository = myOrderRepository;
        }

        public async Task AddMyOrder(MyOrder myOrder)
        {
            await _myOrderRepository.AddMyOrder(myOrder);
        }

        public async Task<IEnumerable<MyOrder>> GetAll()
        {
            return await _myOrderRepository.GetAll();
        }

        public async Task Delete(int id)
        {
            await _myOrderRepository.Delete(id);
        }

        public async Task<IEnumerable<MyOrder>> GetAllByUserId(string userId)
        {
            return await _myOrderRepository.GetAllByUserId(userId);
        }
    }
}
