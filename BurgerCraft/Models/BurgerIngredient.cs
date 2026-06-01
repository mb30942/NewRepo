namespace BurgerCraft.Models
{
    public class BurgerIngredient
    {
        public int BurgerId { get; set; } // Foreign Key for Burger Many-to-One
        public Burger Burger { get; set; }

        public int IngredientId { get; set; } // Foreign Key for Ingredient Many-to-One
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; } = 1; // Default value of 1
    }
}
