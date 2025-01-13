namespace BurgerCraft.Models
{
    public class BurgerIngredient
    {
        public int BurgerId { get; set; } // Foreign Key for Burger
        public Burger Burger { get; set; }

        public int IngredientId { get; set; } // Foreign Key for Ingredient
        public Ingredient Ingredient { get; set; }
        // Add the Quantity property
        public int Quantity { get; set; } = 1; // Default value of 1
    }
}
