using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BurgerCraft.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
namespace BurgerCraft.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "User", Value = "User" }
            };
            // Insert a default empty role at the top of the list
            roles.Insert(0, new SelectListItem { Text = "Select Role", Value = "" });
            var model = new RegisterViewModel
            {
                UserRoleList = roles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var roles = new List<SelectListItem>
            {
                  new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "User", Value = "User" }
            };
            roles.Insert(0, new SelectListItem { Text = "Select Role", Value = "" });
            model.UserRoleList = roles;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(model.UserRole);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.UserRole));
                    }
                    await _userManager.AddToRoleAsync(user, model.UserRole);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Burger");
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Burger");
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction("Lockout", "Account");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(model);
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Burger");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}