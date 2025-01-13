using System.ComponentModel.DataAnnotations;

namespace BurgerCraft.Models
{
    public class Ingredient
    {
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, ErrorMessage = "The Name cannot exceed 50 characters.")]
        public string Name { get; set; } // Name of the ingredient

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, 1000.00, ErrorMessage = "The Price must be between 0.01 and 1000.00.")]
        public decimal Price { get; set; } // Price of the ingredient

        public ICollection<BurgerIngredient> BurgerIngredients { get; set; } = new List<BurgerIngredient>();
    }
}

