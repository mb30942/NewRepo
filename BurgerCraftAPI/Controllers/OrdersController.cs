using BurgerCraftAPI.DTOs.Orders;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraftAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBurgerService _burgerService;
        private readonly IIngredientService _ingredientService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(
            IOrderService orderService,
            IBurgerService burgerService,
            IIngredientService ingredientService,
            UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _burgerService = burgerService;
            _ingredientService = ingredientService;
            _userManager = userManager;
        }

        /// <summary>Get all confirmed orders with enriched details. Admin only.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllOrders();
            var allBurgers = await _burgerService.GetAllBurgers();
            var allIngredients = await _ingredientService.GetAllIngredients();

            var burgerDict = allBurgers.ToDictionary(b => b.Id, b => b.Name);
            var ingredientDict = allIngredients.ToDictionary(i => i.Id, i => i.Name);

            var result = new List<OrderDto>();
            foreach (var order in orders)
            {
                var user = await _userManager.FindByIdAsync(order.UserId);
                result.Add(new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    UserEmail = user?.Email,
                    BurgerId = order.BurgerId,
                    BurgerName = burgerDict.TryGetValue(order.BurgerId, out var bName) ? bName : "Unknown",
                    TotalPrice = order.TotalPrice,
                    Ingredients = order.IngredientIds
                        .Select(id => ingredientDict.TryGetValue(id, out var iName) ? iName : "Unknown")
                        .ToList()
                });
            }

            return Ok(result);
        }
    }
}
