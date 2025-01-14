namespace BurgerCraft.ViewModel
{
    
    public class MyOrderViewModel
    {
        public int Id { get; set; }
        public int BurgerId { get; set; }
        public string BurgerName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<string> Ingredients { get; set; }
    }

}
