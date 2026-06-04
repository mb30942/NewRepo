using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerCraftAPI.Models
{
    public class Burger
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public int BurgerTypeId { get; set; }
        public BurgerType BurgerType { get; set; } = null!;

        public string ImagePath { get; set; } = string.Empty;

        public ICollection<BurgerIngredient> BurgerIngredients { get; set; } = new List<BurgerIngredient>();
    }
}
