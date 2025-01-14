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
        private readonly UserManager<ApplicationUser> _userManager;

        public MyOrderController(IBurgerRepository burgerRepository, IBurgerTypeRepository burgerTypeRepository, IIngredientRepository ingredientRepository, IMyOrderRepository myOrderRepository, UserManager<ApplicationUser> userManager)
        {
            _burgerRepository = burgerRepository;
            _burgerTypeRepository = burgerTypeRepository;
            _ingredientRepository = ingredientRepository;
            _myOrderRepository = myOrderRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _myOrderRepository.GetAll(); 
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


    }
}
