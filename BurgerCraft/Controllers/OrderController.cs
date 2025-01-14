using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraft.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
            private readonly IOrderRepository _orderRepository;
            private readonly IBurgerRepository _burgerRepository;
            private readonly IIngredientRepository _ingredientRepository;

            public OrderController(IOrderRepository orderRepository, IBurgerRepository burgerRepository, IIngredientRepository ingredientRepository)
            {
                _orderRepository = orderRepository;
                _burgerRepository = burgerRepository;
                _ingredientRepository = ingredientRepository;
            }

            public async Task<IActionResult> Index()
            {
                var orders = await _orderRepository.GetAllOrders();

                var allBurgers = await _burgerRepository.GetAllBurgers();
                var allIngredients = await _ingredientRepository.GetAllIngredients();

                var burgerDictionary = allBurgers.ToDictionary(b => b.Id, b => b.Name);
                var ingredientDictionary = allIngredients.ToDictionary(i => i.Id, i => i.Name);

                var enrichedOrders = orders.Select(order => new
                {
                    Order = order,
                    BurgerName = burgerDictionary.ContainsKey(order.BurgerId) ? burgerDictionary[order.BurgerId] : "Unknown Burger",
                    IngredientNames = order.IngredientIds.Select(id => ingredientDictionary.ContainsKey(id) ? ingredientDictionary[id] : "Unknown Ingredient").ToList()
                }).ToList();

                ViewBag.EnrichedOrders = enrichedOrders;

                return View(enrichedOrders);
            }
        }
    }

