namespace BurgerCraftAPI.DTOs.Ingredients
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
