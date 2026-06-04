using BurgerCraftAPI.DTOs.Ingredients;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraftAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        /// <summary>Get all ingredients. Admin only.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAll()
        {
            var ingredients = await _ingredientService.GetAllIngredients();
            return Ok(ingredients.Select(i => new IngredientDto { Id = i.Id, Name = i.Name, Price = i.Price }));
        }

        /// <summary>Get an ingredient by ID. Admin only.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IngredientDto>> GetById(int id)
        {
            var ingredient = await _ingredientService.GetIngredientById(id);
            if (ingredient == null) return NotFound();
            return Ok(new IngredientDto { Id = ingredient.Id, Name = ingredient.Name, Price = ingredient.Price });
        }

        /// <summary>Create a new ingredient. Admin only.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IngredientDto>> Create([FromBody] CreateIngredientDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ingredient = new Ingredient { Name = dto.Name, Price = dto.Price };
            await _ingredientService.AddIngredient(ingredient);
            return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, new IngredientDto { Id = ingredient.Id, Name = ingredient.Name, Price = ingredient.Price });
        }

        /// <summary>Update an ingredient. Admin only.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IngredientDto>> Update(int id, [FromBody] UpdateIngredientDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _ingredientService.GetIngredientById(id);
            if (existing == null) return NotFound();

            var ingredient = new Ingredient { Id = id, Name = dto.Name, Price = dto.Price };
            await _ingredientService.UpdateIngredient(ingredient);
            return Ok(new IngredientDto { Id = id, Name = dto.Name, Price = dto.Price });
        }

        /// <summary>Delete an ingredient. Admin only.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _ingredientService.GetIngredientById(id);
            if (existing == null) return NotFound();

            await _ingredientService.DeleteIngredient(id);
            return NoContent();
        }
    }
}
