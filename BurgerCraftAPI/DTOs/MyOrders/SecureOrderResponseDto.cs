namespace BurgerCraftAPI.DTOs.MyOrders
{
    public class SecureOrderResponseDto
    {
        public decimal TotalPrice { get; set; }
        public int OrderNumber { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
