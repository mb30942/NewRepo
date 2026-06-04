using BurgerCraftAPI.DTOs.BurgerTypes;
using BurgerCraftAPI.Models;
using BurgerCraftAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerCraftAPI.Controllers
{
    [ApiController]
    [Route("api/burger-types")]
    public class BurgerTypesController : ControllerBase
    {
        private readonly IBurgerTypeService _burgerTypeService;

        public BurgerTypesController(IBurgerTypeService burgerTypeService)
        {
            _burgerTypeService = burgerTypeService;
        }

        /// <summary>Get all burger types.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BurgerTypeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BurgerTypeDto>>> GetAll()
        {
            var types = await _burgerTypeService.GetAll();
            return Ok(types.Select(t => new BurgerTypeDto { Id = t.Id, Name = t.Name }));
        }

        /// <summary>Get a burger type by ID.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BurgerTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BurgerTypeDto>> GetById(int id)
        {
            var type = await _burgerTypeService.GetById(id);
            if (type == null) return NotFound();
            return Ok(new BurgerTypeDto { Id = type.Id, Name = type.Name });
        }

        /// <summary>Create a new burger type. Admin only.</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BurgerTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BurgerTypeDto>> Create([FromBody] CreateBurgerTypeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var burgerType = new BurgerType { Name = dto.Name };
            await _burgerTypeService.Add(burgerType);
            return CreatedAtAction(nameof(GetById), new { id = burgerType.Id }, new BurgerTypeDto { Id = burgerType.Id, Name = burgerType.Name });
        }

        /// <summary>Delete a burger type. Admin only.</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _burgerTypeService.GetById(id);
            if (existing == null) return NotFound();

            await _burgerTypeService.Delete(id);
            return NoContent();
        }
    }
}
