using System.ComponentModel.DataAnnotations;

namespace BurgerCraftAPI.DTOs.BurgerTypes
{
    public class CreateBurgerTypeDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
