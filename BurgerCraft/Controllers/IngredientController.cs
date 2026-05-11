using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BurgerCraft.Controllers
{
    [Authorize(Roles = "Admin")] // Restrict access to Admins only
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        // GET: Ingredient/Index
        public async Task<IActionResult> Index()
        {
            var ingredients = await _ingredientService.GetAllIngredients();
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
                await _ingredientService.AddIngredient(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredient/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _ingredientService.GetIngredientById(id);
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
                await _ingredientService.UpdateIngredient(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredient/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _ingredientService.GetIngredientById(id);
            if (ingredient == null)
                return NotFound();

            return View(ingredient);
        }

        // POST: Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ingredientService.DeleteIngredient(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
