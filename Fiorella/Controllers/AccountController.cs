using Fiorella.Models;
using Fiorella.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fiorella.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, true);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var dbUser = await _userManager.FindByNameAsync(registerModel.UserName);
            if (dbUser != null)
            {
                ModelState.AddModelError(nameof(RegisterViewModel.UserName), 
                    "The user with this username is already exits.");
                return View();
            }

            User user = new User
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                FullName = registerModel.FullName,
            };

            var identityResult = await _userManager.CreateAsync(user, registerModel.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
