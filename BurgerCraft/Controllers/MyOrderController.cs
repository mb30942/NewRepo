using BurgerCraft.Models;
using BurgerCraft.Services.Interfaces;
using BurgerCraft.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraft.Controllers
{
    public class MyOrderController : Controller
    {
        private readonly IMyOrderService _myOrderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyOrderController(IMyOrderService myOrderService, UserManager<ApplicationUser> userManager)
        {
            _myOrderService = myOrderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var ordersWithDetails = await _myOrderService.GetEnrichedOrdersByUserId(user.Id);

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

            var (totalPrice, uniqueOrderNumber) = await _myOrderService.SecureOrderAsync(user.Id);

            return RedirectToAction("OrderConfirmation", new { totalPrice = totalPrice, orderNumber = uniqueOrderNumber });
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