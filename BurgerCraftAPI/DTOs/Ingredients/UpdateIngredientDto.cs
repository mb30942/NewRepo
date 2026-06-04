using System.ComponentModel.DataAnnotations;

namespace BurgerCraftAPI.DTOs.Ingredients
{
    public class UpdateIngredientDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000, ErrorMessage = "Price must be between $0.01 and $1000.")]
        public decimal Price { get; set; }
    }
}
