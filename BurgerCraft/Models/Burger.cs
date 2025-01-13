using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BurgerCraft.Models
{
    public class Burger
    {
        [Key]
        public int Id { get; set; } 
        [Required(ErrorMessage = "Burger name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } 
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000, ErrorMessage = "Price must be between $0.01 and $1000.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; } 
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } 
        [Required(ErrorMessage = "Burger type is required.")]
        public int BurgerTypeId { get; set; } 
        [ForeignKey("BurgerTypeId")]
        public BurgerType BurgerType { get; set; } 
        [Required(ErrorMessage = "Image path is required.")]
        public string ImagePath { get; set; }

        public ICollection<BurgerIngredient> BurgerIngredients { get; set; } = new List<BurgerIngredient>();
    }
}

