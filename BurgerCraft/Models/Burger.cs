namespace BurgerCraft.Models
{
    public class Burger
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Burger Name (e.g., "Veggie Delight")
        public decimal Price { get; set; } // Burger Price
        public string Description { get; set; } // Description of the burger

        // Foreign Key for BurgerType
        public int BurgerTypeId { get; set; } // Foreign Key
        public BurgerType BurgerType { get; set; } // Navigation Property
    }
}
