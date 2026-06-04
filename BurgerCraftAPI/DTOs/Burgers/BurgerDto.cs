using BurgerCraftAPI.DTOs.Ingredients;

namespace BurgerCraftAPI.DTOs.Burgers
{
    public class BurgerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public bool IsDiscountActive { get; set; }
        public string? Description { get; set; }
        public int BurgerTypeId { get; set; }
        public string BurgerTypeName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public List<IngredientDto> Ingredients { get; set; } = new();
    }
}
