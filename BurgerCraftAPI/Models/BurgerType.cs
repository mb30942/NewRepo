using System.ComponentModel.DataAnnotations;

namespace BurgerCraftAPI.Models
{
    public class BurgerType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Burger> Burgers { get; set; } = new List<Burger>();
    }
}
