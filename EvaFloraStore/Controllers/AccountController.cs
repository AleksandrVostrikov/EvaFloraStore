using EvaFloraStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EvaFloraStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public ViewResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var signResult = await _signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false);
                    if (signResult.Succeeded)
                    {
                        return RedirectToAction("GetOrders", "AdminOrder");
                    }
                }
            }
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(loginModel);
        }
        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
