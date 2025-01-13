using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BurgerCraft.Controllers
{
    public class BurgerController : Controller
    {
        private readonly IBurgerRepository _burgerRepository;
        private readonly IBurgerTypeRepository _burgerTypeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public BurgerController(IBurgerRepository burgerRepository, IBurgerTypeRepository burgerTypeRepository, IIngredientRepository ingredientRepository)
        {
            _burgerRepository = burgerRepository;
            _burgerTypeRepository= burgerTypeRepository;
            _ingredientRepository= ingredientRepository;
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
    }
}

