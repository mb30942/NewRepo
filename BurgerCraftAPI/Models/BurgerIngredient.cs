namespace BurgerCraftAPI.Models
{
    public class BurgerIngredient
    {
        public int BurgerId { get; set; }
        public Burger Burger { get; set; } = null!;

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public int Quantity { get; set; } = 1;
    }
}
