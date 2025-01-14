using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BurgerCraft.Controllers
{
    public class BurgerController : Controller
    {
        private readonly IBurgerRepository _burgerRepository;
        private readonly IBurgerTypeRepository _burgerTypeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<BurgerController> _logger;

        public BurgerController(IBurgerRepository burgerRepository, IBurgerTypeRepository burgerTypeRepository, IIngredientRepository ingredientRepository, IOrderRepository orderRepository, UserManager<ApplicationUser> userManager, ILogger<BurgerController> logger)
        {
            _burgerRepository = burgerRepository;
            _burgerTypeRepository = burgerTypeRepository;
            _ingredientRepository = ingredientRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var burgers = await _burgerRepository.GetAllBurgers();
            return View(burgers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var burger = await _burgerRepository.GetBurgerById(id);

            if (burger == null)
            {
                return NotFound(); 
            }

            return View(burger);
        }
        //public async Task<IActionResult> Order(int id)
        //{
        //    var burger = await _burgerRepository.GetBurgerById(id);

        //    if (burger == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(burger);
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var burgerTypes = await Task.Run(() => _burgerTypeRepository.GetAll());
            var ingredients = await Task.Run(() => _ingredientRepository.GetAllIngredients());

            ViewBag.BurgerTypes = new SelectList(burgerTypes, "Id", "Name");

            ViewBag.Ingredients = ingredients;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Burger burger, int[] selectedIngredients)
        {
            //Add into BurgerIngredients
            if (selectedIngredients != null && selectedIngredients.Length > 0)
            {
                foreach (var ingredientId in selectedIngredients)
                {
                    burger.BurgerIngredients.Add(new BurgerIngredient
                    {
                        IngredientId = ingredientId 
                    });
                }
            }

            try
            {
                await _burgerRepository.AddBurger(burger);
                Console.WriteLine("Burger saved successfully.");
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
            _burgerRepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Entering Edit method with id: {Id}", id);

            var burger = await _burgerRepository.GetBurgerById(id);

            var burgerTypes = await Task.Run(() => _burgerTypeRepository.GetAll());
            var ingredients = await Task.Run(() => _ingredientRepository.GetAllIngredients());

            // Get selected ingredient ids
            var selectedIngredients = burger.BurgerIngredients.Select(bi => bi.IngredientId).ToArray();

            ViewBag.BurgerTypes = new SelectList(burgerTypes, "Id", "Name", burger.BurgerTypeId);
            ViewBag.Ingredients = ingredients;
            ViewBag.SelectedIngredients = selectedIngredients;  // Pass selected ingredients

            return View(burger);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Burger burger, int[] selectedIngredients)
        {

                // Add ingredients to BurgerIngredients
                if (selectedIngredients != null && selectedIngredients.Length > 0)
                {
                    burger.BurgerIngredients = selectedIngredients.Select(ingredientId => new BurgerIngredient
                    {
                        IngredientId = ingredientId
                    }).ToList();
                }

                try
                {
                    await _burgerRepository.UpdateBurger(burger);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating burger: {ex.Message}");
                }
            
            // Get the necessary data for the dropdown again
            var burgerTypes = await Task.Run(() => _burgerTypeRepository.GetAll());
            var ingredients = await Task.Run(() => _ingredientRepository.GetAllIngredients());

            ViewBag.BurgerTypes = new SelectList(burgerTypes, "Id", "Name", burger.BurgerTypeId);
            ViewBag.Ingredients = ingredients;

            return View(burger);
        }

        public async Task<IActionResult> Order(int id)
        {
            var burger = await _burgerRepository.GetBurgerById(id);

            if (burger == null)
            {
                return NotFound();
            }

            ViewBag.AllIngredients = await _ingredientRepository.GetAllIngredients();
            return View(burger);
        }
        [HttpPost]
        public async Task<IActionResult> Order(int BurgerId, int Quantity, List<int> SelectedIngredients)
        {
            var burger = await _burgerRepository.GetBurgerById(BurgerId);

            if (burger == null)
            {
                return NotFound();
            }

            var totalPrice = burger.Price * Quantity;

            List<int> ingredientIds;
            decimal ingredientsTotal = 0;

            if (SelectedIngredients != null && SelectedIngredients.Any())
            {
                ingredientIds = SelectedIngredients;

                foreach (var ingredientId in SelectedIngredients)
                {
                    var ingredient = await _ingredientRepository.GetIngredientById(ingredientId);
                    if (ingredient != null)
                    {
                        ingredientsTotal += ingredient.Price;
                    }
                }
                totalPrice += ingredientsTotal;
            }
            else
            {
                ingredientIds = burger.BurgerIngredients.Select(bi => bi.IngredientId).ToList();

                foreach (var ingredientId in ingredientIds)
                {
                    var ingredient = await _ingredientRepository.GetIngredientById(ingredientId);
                    if (ingredient != null)
                    {
                        ingredientsTotal += ingredient.Price;
                    }
                }
            }



            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); 
            }

            var order = new Order
            {
                UserId = user.Id,
                BurgerId = BurgerId,
                IngredientIds = ingredientIds, 
                TotalPrice = totalPrice,
            };

            if (ModelState.IsValid)
            {
                await _orderRepository.AddOrder(order); 
                return RedirectToAction("Index", "Burger"); 
            }

            return View(order);
        }


    }
}

