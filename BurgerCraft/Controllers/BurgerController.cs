using BurgerCraft.Models;
using BurgerCraft.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BurgerCraft.Controllers
{
    public class BurgerController : Controller
    {
        private readonly IBurgerService _burgerService;
        private readonly IBurgerTypeService _burgerTypeService;
        private readonly IIngredientService _ingredientService;
        private readonly IMyOrderService _myOrderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BurgerController> _logger;
        private readonly ITimeSensitiveOfferService _offerService;

        public BurgerController(IBurgerService burgerService, IBurgerTypeService burgerTypeService,
            IIngredientService ingredientService, UserManager<ApplicationUser> userManager,
            ILogger<BurgerController> logger, IMyOrderService myOrderService,
            ITimeSensitiveOfferService offerService)
        {
            _burgerService = burgerService;
            _burgerTypeService = burgerTypeService;
            _ingredientService = ingredientService;
            _myOrderService = myOrderService;
            _userManager = userManager;
            _logger = logger;
            _offerService = offerService;
        }

        public async Task<IActionResult> Index(int? burgerTypeId, string searchQuery)
        {
            var burgerTypes = _burgerTypeService.GetAll();

            var burgers = burgerTypeId.HasValue
                ? await _burgerService.GetBurgersByTypeWithDiscount(burgerTypeId.Value)
                : await _burgerService.GetAllBurgersWithDiscount();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                burgers = burgers.Where(b => b.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.IsOfferActive = _offerService.IsTimeSensitiveOfferActive();
            ViewBag.BurgerTypes = new SelectList(burgerTypes, "Id", "Name", burgerTypeId);

            return View(burgers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var burger = await _burgerService.GetBurgerByIdWithDiscount(id);

            if (burger == null)
            {
                return NotFound();
            }

            return View(burger);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var burgerTypes = await Task.Run(() => _burgerTypeService.GetAll());
            var ingredients = await Task.Run(() => _ingredientService.GetAllIngredients());

            ViewBag.BurgerTypes = new SelectList(burgerTypes, "Id", "Name");
            ViewBag.Ingredients = ingredients;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Burger burger, int[] selectedIngredients, IFormFile ImageFile)
        {
            if (selectedIngredients != null && selectedIngredients.Length > 0)
            {
                foreach (var ingredientId in selectedIngredients)
                {
                    burger.BurgerIngredients.Add(new BurgerIngredient { IngredientId = ingredientId });
                }
            }

            try
            {
                // Image saving is now handled inside BurgerService.AddBurger
                await _burgerService.AddBurger(burger, ImageFile);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving burger: {ex.Message}");
                return View(burger);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _burgerService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Entering Edit method with id: {Id}", id);

            var burger = await _burgerService.GetBurgerById(id);
            var burgerTypes = await Task.Run(() => _burgerTypeService.GetAll());
            var ingredients = await Task.Run(() => _ingredientService.GetAllIngredients());
            var selectedIngredients = burger.BurgerIngredients.Select(bi => bi.IngredientId).ToArray();

            ViewBag.BurgerTypes = new SelectList(burgerTypes, "Id", "Name", burger.BurgerTypeId);
            ViewBag.Ingredients = ingredients;
            ViewBag.SelectedIngredients = selectedIngredients;

            return View(burger);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Burger burger, int[] selectedIngredients, IFormFile ImageFile)
        {
            if (selectedIngredients != null && selectedIngredients.Length > 0)
            {
                burger.BurgerIngredients = selectedIngredients.Select(ingredientId => new BurgerIngredient
                {
                    IngredientId = ingredientId
                }).ToList();
            }

            try
            {
                // Image saving is now handled inside BurgerService.UpdateBurger
                await _burgerService.UpdateBurger(burger, ImageFile);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating burger: {ex.Message}");
            }

            var burgerTypes = await Task.Run(() => _burgerTypeService.GetAll());
            var ingredients = await Task.Run(() => _ingredientService.GetAllIngredients());

            ViewBag.BurgerTypes = new SelectList(burgerTypes, "Id", "Name", burger.BurgerTypeId);
            ViewBag.Ingredients = ingredients;

            return View(burger);
        }

        public async Task<IActionResult> Order(int id)
        {
            // Discount applied inside service
            var burger = await _burgerService.GetBurgerByIdWithDiscount(id);

            if (burger == null)
            {
                return NotFound();
            }

            ViewBag.AllIngredients = await _ingredientService.GetAllIngredients();
            return View(burger);
        }

        [HttpPost]
        public async Task<IActionResult> Order(int BurgerId, int Quantity, List<int> SelectedIngredients)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                var myOrder = await _myOrderService.PrepareOrderAsync(user.Id, BurgerId, Quantity, SelectedIngredients);

                if (ModelState.IsValid)
                {
                    await _myOrderService.AddMyOrder(myOrder);
                    return RedirectToAction("Index", "Burger");
                }

                return View(myOrder);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}