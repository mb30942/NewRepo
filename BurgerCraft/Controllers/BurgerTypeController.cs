using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraft.Controllers
{
    public class BurgerTypeController : Controller
    {
        private readonly IBurgerTypeRepository _burgerTypeRepository;
        public BurgerTypeController(IBurgerTypeRepository burgerTypeRepository)
        {
            _burgerTypeRepository = burgerTypeRepository;
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
                _burgerTypeRepository.Add(burgerType); 
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
            var burgerType = _burgerTypeRepository.GetById(id);

            if (burgerType != null)
            {
                //Console.WriteLine(burgerType);
                _burgerTypeRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var burgerTypes = _burgerTypeRepository.GetAll();
            return View(burgerTypes);
        }

    }
}
