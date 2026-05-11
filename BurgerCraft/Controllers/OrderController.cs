using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using BurgerCraft.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerCraft.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
            private readonly IOrderService _orderService;
            private readonly IBurgerService _burgerService;
            private readonly IIngredientService _ingredientService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, IBurgerService burgerService, IIngredientService ingredientService,
        UserManager<ApplicationUser> userManager)
            {
                _orderService = orderService;
                _burgerService = burgerService;
                _ingredientService = ingredientService;
            _userManager = userManager;
        }

            public async Task<IActionResult> Index()
            {
                var orders = await _orderService.GetAllOrders();

                var allBurgers = await _burgerService.GetAllBurgers();
                var allIngredients = await _ingredientService.GetAllIngredients();

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

