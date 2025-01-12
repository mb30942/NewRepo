using System;
using System.ComponentModel.DataAnnotations;

namespace BurgerCraft.Models
{
    public class BurgerType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Type name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } 

        // Navigation Property
        public ICollection<Burger> Burgers { get; set; } = new List<Burger>();

    }
}
