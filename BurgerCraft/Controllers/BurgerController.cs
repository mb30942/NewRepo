using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraft.Controllers
{
    public class BurgerController : Controller
    {
        private readonly IBurgerRepository _burgerRepository;

        public BurgerController(IBurgerRepository burgerRepository)
        {
            _burgerRepository = burgerRepository;
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
    }
}
