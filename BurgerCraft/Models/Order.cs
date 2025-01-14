using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BurgerCraft.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string UserId { get; set; }

        [Required] 
        public int BurgerId { get; set; } 
        public List<int> IngredientIds { get; set; } 

        [Required] 
        [Column(TypeName = "decimal(18,2)")] 
        public decimal TotalPrice { get; set; } 
    }
}
