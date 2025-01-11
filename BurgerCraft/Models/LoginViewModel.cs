using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace BurgerCraft.Models
{
    public class LoginViewModel
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
        [Display(Name = "User Role")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}