using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraft.Controllers
{
    public class BurgerTypeController : Controller
    {
        private readonly IBurgerTypeService _burgerTypeService;
        public BurgerTypeController(IBurgerTypeService burgerTypeService)
        {
            _burgerTypeService = burgerTypeService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Render the Create view
            return View();
        }

        [HttpPost]
        public IActionResult Create(BurgerType burgerType)
        {
            if (burgerType.Burgers == null)
            {
                burgerType.Burgers = new List<Burger>();
            }

            if (ModelState.IsValid)
            {
                _burgerTypeService.Add(burgerType); 
                return RedirectToAction("Index"); 
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error: {error.ErrorMessage}"); 
            }

            return View(burgerType);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var burgerType = _burgerTypeService.GetById(id);

            if (burgerType != null)
            {
                //Console.WriteLine(burgerType);
                _burgerTypeService.Delete(id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var burgerTypes = _burgerTypeService.GetAll();
            return View(burgerTypes);
        }

    }
}
