namespace BurgerCraftAPI.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public int BurgerId { get; set; }
        public string BurgerName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<string> Ingredients { get; set; } = new();
    }
}
