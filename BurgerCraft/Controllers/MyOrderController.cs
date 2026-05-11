using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using BurgerCraft.Services.Interfaces;
using BurgerCraft.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraft.Controllers
{
    public class MyOrderController : Controller
    {
        private readonly IBurgerService _burgerService;
        private readonly IBurgerTypeService _burgerTypeService;
        private readonly IIngredientService _ingredientService;
        private readonly IMyOrderService _myOrderService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyOrderController(IBurgerService burgerService, IBurgerTypeService burgerTypeService, IIngredientService ingredientService, IMyOrderService myOrderService, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _burgerService = burgerService;
            _burgerTypeService = burgerTypeService;
            _ingredientService = ingredientService;
            _myOrderService = myOrderService;
            _orderService = orderService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var orders = await _myOrderService.GetAllByUserId(user.Id);
            var allIngredients = await _ingredientService.GetAllIngredients();  
            var allBurgers = await _burgerService.GetAllBurgers();  

            
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
            _myOrderService.Delete(id);
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

            
            var myOrders = await _myOrderService.GetAllByUserId(user.Id);
            
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

                var createdOrder = order;
                await _orderService.AddOrder(order);
                orderIds.Add(createdOrder.Id);
                totalPrice += myOrder.TotalPrice;

            }

            int uniqueOrderNumber = GenerateUniqueOrderNumber();

            foreach (var myOrder in myOrders)
            {
                await _myOrderService.Delete(myOrder.Id);
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

            var orders = await _myOrderService.GetAllByUserId(user.Id);
            var totalPrice = orders.Sum(order => order.TotalPrice);

            return PartialView("_OrderTotal", totalPrice);
        }




    }
}
