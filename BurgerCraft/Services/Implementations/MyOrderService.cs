using BurgerCraft.Models;
using BurgerCraft.Repositories.Interfaces;
using BurgerCraft.Services.Interfaces;

namespace BurgerCraft.Services.Implementations
{
    public class MyOrderService : IMyOrderService
    {
        private readonly IMyOrderRepository _myOrderRepository;
        private readonly IBurgerService _burgerService;
        private readonly IIngredientService _ingredientService;
        private readonly ITimeSensitiveOfferService _offerService;
        private readonly IOrderService _orderService;

        public MyOrderService(
            IMyOrderRepository myOrderRepository,
            IBurgerService burgerService,
            IIngredientService ingredientService,
            ITimeSensitiveOfferService offerService,
            IOrderService orderService)
        {
            _myOrderRepository = myOrderRepository;
            _burgerService = burgerService;
            _ingredientService = ingredientService;
            _offerService = offerService;
            _orderService = orderService;
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

        public async Task<MyOrder> PrepareOrderAsync(string userId, int burgerId, int quantity, List<int> selectedIngredients)
        {
            var burger = await _burgerService.GetBurgerById(burgerId);

            if (burger == null)
            {
                throw new KeyNotFoundException($"Burger with ID {burgerId} was not found.");
            }

            var discountedPrice = _offerService.ApplyDiscount(burger.Price);
            var totalPrice = discountedPrice * quantity;

            List<int> ingredientIds;
            decimal ingredientsTotal = 0;

            if (selectedIngredients != null && selectedIngredients.Any())
            {
                ingredientIds = selectedIngredients;

                foreach (var ingredientId in selectedIngredients)
                {
                    var ingredient = await _ingredientService.GetIngredientById(ingredientId);
                    if (ingredient != null)
                    {
                        ingredientsTotal += ingredient.Price;
                    }
                }
                totalPrice += ingredientsTotal;
            }
            else
            {
                ingredientIds = burger.BurgerIngredients.Select(bi => bi.IngredientId).ToList();

                foreach (var ingredientId in ingredientIds)
                {
                    var ingredient = await _ingredientService.GetIngredientById(ingredientId);
                    if (ingredient != null)
                    {
                        ingredientsTotal += ingredient.Price;
                    }
                }
            }

            return new MyOrder
            {
                UserId = userId,
                BurgerId = burgerId,
                IngredientIds = ingredientIds,
                TotalPrice = totalPrice,
            };
        }

        public async Task<(decimal TotalPrice, int OrderNumber)> SecureOrderAsync(string userId)
        {
            var myOrders = await _myOrderRepository.GetAllByUserId(userId);
            
            decimal totalPrice = 0;

            foreach (var myOrder in myOrders)
            {
                var order = new Order
                {
                    UserId = userId,
                    BurgerId = myOrder.BurgerId,
                    IngredientIds = myOrder.IngredientIds,
                    TotalPrice = myOrder.TotalPrice
                };

                await _orderService.AddOrder(order);
                totalPrice += myOrder.TotalPrice;
            }

            Random random = new Random();
            int orderNumber = random.Next(10000, 99999);

            foreach (var myOrder in myOrders)
            {
                await _myOrderRepository.Delete(myOrder.Id);
            }

            return (totalPrice, orderNumber);
        }
    }
}
