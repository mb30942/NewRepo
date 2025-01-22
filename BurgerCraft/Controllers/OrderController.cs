using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraft.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
            private readonly IOrderRepository _orderRepository;
            private readonly IBurgerRepository _burgerRepository;
            private readonly IIngredientRepository _ingredientRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderRepository orderRepository, IBurgerRepository burgerRepository, IIngredientRepository ingredientRepository,
        UserManager<ApplicationUser> userManager)
            {
                _orderRepository = orderRepository;
                _burgerRepository = burgerRepository;
                _ingredientRepository = ingredientRepository;
            _userManager = userManager;
        }

            public async Task<IActionResult> Index()
            {
                var orders = await _orderRepository.GetAllOrders();

                var allBurgers = await _burgerRepository.GetAllBurgers();
                var allIngredients = await _ingredientRepository.GetAllIngredients();

                var burgerDictionary = allBurgers.ToDictionary(b => b.Id, b => b.Name);
                var ingredientDictionary = allIngredients.ToDictionary(i => i.Id, i => i.Name);
            
            var userIds = orders.Select(o => o.UserId).Distinct().ToList();

          
            var users = await _userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.UserName);

            var enrichedOrders = orders.Select(order => new
                {
                Order = order,
                BurgerName = burgerDictionary.ContainsKey(order.BurgerId) ? burgerDictionary[order.BurgerId] : "Unknown Burger",
                IngredientNames = order.IngredientIds.Select(id => ingredientDictionary.ContainsKey(id) ? ingredientDictionary[id] : "Unknown Ingredient").ToList()
            }).ToList();


            ViewBag.EnrichedOrders = enrichedOrders;
            ViewBag.Usernames = users;
            return View(enrichedOrders);
            }
        }
    }

