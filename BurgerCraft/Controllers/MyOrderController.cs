using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using BurgerCraft.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraft.Controllers
{
    public class MyOrderController : Controller
    {
        private readonly IBurgerRepository _burgerRepository;
        private readonly IBurgerTypeRepository _burgerTypeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMyOrderRepository _myOrderRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyOrderController(IBurgerRepository burgerRepository, IBurgerTypeRepository burgerTypeRepository, IIngredientRepository ingredientRepository, IMyOrderRepository myOrderRepository, IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            _burgerRepository = burgerRepository;
            _burgerTypeRepository = burgerTypeRepository;
            _ingredientRepository = ingredientRepository;
            _myOrderRepository = myOrderRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var orders = await _myOrderRepository.GetAllByUserId(user.Id);
            var allIngredients = await _ingredientRepository.GetAllIngredients();  
            var allBurgers = await _burgerRepository.GetAllBurgers();  

            
            var ingredientDictionary = allIngredients.ToDictionary(i => i.Id, i => i.Name);

           
            var burgerDictionary = allBurgers.ToDictionary(b => b.Id, b => b.Name);

            
            var ordersWithDetails = orders.Select(order => new MyOrderViewModel
            {
                Id = order.Id,
                BurgerId = order.BurgerId,
                BurgerName = burgerDictionary.ContainsKey(order.BurgerId) ? burgerDictionary[order.BurgerId] : "Unknown Burger",
                TotalPrice = order.TotalPrice,
                Ingredients = order.IngredientIds.Select(id => ingredientDictionary.ContainsKey(id) ? ingredientDictionary[id] : "Unknown").ToList()
            }).ToList();
           
            var totalAllOrdersPrice = ordersWithDetails.Sum(order => order.TotalPrice);

            ViewBag.TotalAllOrdersPrice = totalAllOrdersPrice;
            return View(ordersWithDetails);  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _myOrderRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SecureOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            
            var myOrders = await _myOrderRepository.GetAllByUserId(user.Id);
            
            var orderIds = new List<int>();
            decimal totalPrice = 0;

            foreach (var myOrder in myOrders)
            {
                var order = new Order
                {
                    UserId = user.Id,
                    BurgerId = myOrder.BurgerId,
                    IngredientIds = myOrder.IngredientIds,
                    TotalPrice = myOrder.TotalPrice
                };

                var createdOrder =  _orderRepository.AddOrder(order);
                orderIds.Add(createdOrder.Id);
                totalPrice += myOrder.TotalPrice;

            }

            int uniqueOrderNumber = GenerateUniqueOrderNumber();

            foreach (var myOrder in myOrders)
            {
                await _myOrderRepository.Delete(myOrder.Id);
            }

            return RedirectToAction("OrderConfirmation", new { totalPrice = totalPrice, orderNumber = uniqueOrderNumber });
        }

        private int GenerateUniqueOrderNumber()
        {
            Random random = new Random();
            int orderNumber;

                orderNumber = random.Next(10000, 99999);
            

            return orderNumber;
        }
        public IActionResult OrderConfirmation(decimal totalPrice, int orderNumber)
        {
            ViewBag.TotalPrice = totalPrice;
            ViewBag.OrderNumber = orderNumber;
            return View();
        }

        public async Task<IActionResult> GetTotalPrice()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orders = await _myOrderRepository.GetAllByUserId(user.Id);
            var totalPrice = orders.Sum(order => order.TotalPrice);

            return PartialView("_OrderTotal", totalPrice);
        }




    }
}
