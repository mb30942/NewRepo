using System.ComponentModel.DataAnnotations;

namespace BurgerCraftAPI.DTOs.Burgers
{
    public class CreateBurgerDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000, ErrorMessage = "Price must be between $0.01 and $1000.")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int BurgerTypeId { get; set; }

        public IFormFile? Image { get; set; }

        public List<int> IngredientIds { get; set; } = new();
    }
}
