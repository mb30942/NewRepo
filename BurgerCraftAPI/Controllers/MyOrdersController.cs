using BurgerCraftAPI.DTOs.MyOrders;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BurgerCraftAPI.Controllers
{
    [ApiController]
    [Route("api/my-orders")]
    [Authorize]
    public class MyOrdersController : ControllerBase
    {
        private readonly IMyOrderService _myOrderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyOrdersController(IMyOrderService myOrderService, UserManager<ApplicationUser> userManager)
        {
            _myOrderService = myOrderService;
            _userManager = userManager;
        }

        /// <summary>Get the current user's cart (pending orders).</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MyOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<MyOrderDto>>> GetMyOrders()
        {
            var userId = GetUserId();
            var orders = await _myOrderService.GetEnrichedOrdersByUserId(userId);
            return Ok(orders);
        }

        /// <summary>Get total price for the current user's cart.</summary>
        [HttpGet("total")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetTotal()
        {
            var userId = GetUserId();
            var orders = await _myOrderService.GetAllByUserId(userId);
            var total = orders.Sum(o => o.TotalPrice);
            return Ok(new { totalPrice = total });
        }

        /// <summary>Add a burger to the cart.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(MyOrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MyOrderDto>> AddToCart([FromBody] CreateMyOrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserId();
            try
            {
                var myOrder = await _myOrderService.PrepareOrderAsync(
                    userId, dto.BurgerId, dto.Quantity, dto.SelectedIngredientIds);

                await _myOrderService.AddMyOrder(myOrder);

                var enriched = await _myOrderService.GetEnrichedOrdersByUserId(userId);
                var created = enriched.FirstOrDefault(o => o.Id == myOrder.Id);
                return CreatedAtAction(nameof(GetMyOrders), created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>Remove an item from the cart.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var orders = await _myOrderService.GetAllByUserId(userId);
            var order = orders.FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();
            if (order.UserId != userId) return Forbid();

            await _myOrderService.Delete(id);
            return NoContent();
        }

        /// <summary>Checkout: convert cart to confirmed orders and generate an order number.</summary>
        [HttpPost("checkout")]
        [ProducesResponseType(typeof(SecureOrderResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SecureOrderResponseDto>> Checkout()
        {
            var userId = GetUserId();
            var pendingOrders = await _myOrderService.GetAllByUserId(userId);

            if (!pendingOrders.Any())
                return BadRequest(new { message = "Your cart is empty." });

            var (totalPrice, orderNumber) = await _myOrderService.SecureOrderAsync(userId);

            return Ok(new SecureOrderResponseDto
            {
                TotalPrice = totalPrice,
                OrderNumber = orderNumber,
                Message = $"Order #{orderNumber} confirmed! Total: ${totalPrice:F2}"
            });
        }

        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
