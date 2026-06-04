namespace BurgerCraftAPI.DTOs.MyOrders
{
    public class MyOrderDto
    {
        public int Id { get; set; }
        public int BurgerId { get; set; }
        public string BurgerName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<string> Ingredients { get; set; } = new();
    }
}
