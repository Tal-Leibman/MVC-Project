using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class LoginController : Controller
    {
        private SignInManager<User> _signInManager;

        public LoginController(SignInManager<User> signInManager) => _signInManager = signInManager;

        public IActionResult Index(bool failedLogin = false)
        {
            ViewBag.SelectedNavigation = "login-index-nav";
            ViewBag.BadLogin = failedLogin;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            var res = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            return res == Microsoft.AspNetCore.Identity.SignInResult.Success ? RedirectToAction("Index", "Home") : Index(true);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}