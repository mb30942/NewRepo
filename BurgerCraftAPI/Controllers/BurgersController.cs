using BurgerCraftAPI.DTOs.Burgers;
using BurgerCraftAPI.DTOs.Ingredients;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraftAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BurgersController : ControllerBase
    {
        private readonly IBurgerService _burgerService;
        private readonly ITimeSensitiveOfferService _offerService;

        public BurgersController(IBurgerService burgerService, ITimeSensitiveOfferService offerService)
        {
            _burgerService = burgerService;
            _offerService = offerService;
        }

        /// <summary>Get all burgers. Supports filtering by type and searching by name.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BurgerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BurgerDto>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] int? typeId)
        {
            var burgers = typeId.HasValue
                ? await _burgerService.GetBurgersByType(typeId.Value)
                : await _burgerService.GetAllBurgers();

            if (!string.IsNullOrWhiteSpace(search))
                burgers = burgers.Where(b => b.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            return Ok(burgers.Select(MapToDto));
        }

        /// <summary>Get a burger by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BurgerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BurgerDto>> GetById(int id)
        {
            var burger = await _burgerService.GetBurgerById(id);
            if (burger == null) return NotFound();
            return Ok(MapToDto(burger));
        }

        /// <summary>Create a new burger. Admin only.</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(BurgerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BurgerDto>> Create([FromForm] CreateBurgerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var burger = new Burger
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                BurgerTypeId = dto.BurgerTypeId,
                BurgerIngredients = dto.IngredientIds.Select(id => new BurgerIngredient { IngredientId = id }).ToList()
            };

            await _burgerService.AddBurger(burger, dto.Image);
            return CreatedAtAction(nameof(GetById), new { id = burger.Id }, MapToDto(burger));
        }

        /// <summary>Update an existing burger. Admin only.</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(BurgerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BurgerDto>> Update(int id, [FromForm] UpdateBurgerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _burgerService.GetBurgerById(id);
            if (existing == null) return NotFound();

            var burger = new Burger
            {
                Id = id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                BurgerTypeId = dto.BurgerTypeId,
                ImagePath = existing.ImagePath,
                BurgerIngredients = dto.IngredientIds.Select(iId => new BurgerIngredient { IngredientId = iId }).ToList()
            };

            await _burgerService.UpdateBurger(burger, dto.Image);
            var updated = await _burgerService.GetBurgerById(id);
            return Ok(MapToDto(updated!));
        }

        /// <summary>Delete a burger. Admin only.</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _burgerService.GetBurgerById(id);
            if (existing == null) return NotFound();

            await _burgerService.Delete(id);
            return NoContent();
        }

        private BurgerDto MapToDto(Burger burger)
        {
            var isDiscountActive = _offerService.IsTimeSensitiveOfferActive();
            return new BurgerDto
            {
                Id = burger.Id,
                Name = burger.Name,
                Price = burger.Price,
                DiscountedPrice = isDiscountActive ? _offerService.ApplyDiscount(burger.Price) : null,
                IsDiscountActive = isDiscountActive,
                Description = burger.Description,
                BurgerTypeId = burger.BurgerTypeId,
                BurgerTypeName = burger.BurgerType?.Name ?? string.Empty,
                ImagePath = burger.ImagePath,
                Ingredients = burger.BurgerIngredients
                    .Select(bi => new IngredientDto
                    {
                        Id = bi.IngredientId,
                        Name = bi.Ingredient?.Name ?? string.Empty,
                        Price = bi.Ingredient?.Price ?? 0
                    }).ToList()
            };
        }
    }
}
