using System.ComponentModel.DataAnnotations;

namespace BurgerCraftAPI.DTOs.MyOrders
{
    public class CreateMyOrderDto
    {
        [Required]
        public int BurgerId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;

        public List<int> SelectedIngredientIds { get; set; } = new();
    }
}
