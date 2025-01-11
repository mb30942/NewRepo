using System;

namespace BurgerCraft.Models
{
    public class BurgerType
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Name of the type (e.g., "Veggie", "Chicken", "Beef")

        // Navigation Property
        public ICollection<Burger> Burgers { get; set; } // One BurgerType can have many Burgers

    }
}
