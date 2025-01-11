using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace BurgerCraft.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "User Role")]
        public string UserRole { get; set; }
        public IEnumerable<SelectListItem>? UserRoleList { get; set; }
    }
}