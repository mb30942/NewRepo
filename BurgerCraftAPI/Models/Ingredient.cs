using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerCraftAPI.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        public ICollection<BurgerIngredient> BurgerIngredients { get; set; } = new List<BurgerIngredient>();
    }
}
