using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BurgerCraft.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to Admins only
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET: Ingredient/Index
        public async Task<IActionResult> Index()
        {
            var ingredients = await _ingredientRepository.GetAllIngredients();
            return View(ingredients);
        }

        // GET: Ingredient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await _ingredientRepository.AddIngredient(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredient/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _ingredientRepository.GetIngredientById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _ingredientRepository.UpdateIngredient(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredient/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _ingredientRepository.GetIngredientById(id);
            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        // POST: Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ingredientRepository.DeleteIngredient(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
