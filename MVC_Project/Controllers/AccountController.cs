using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> signInManager;

        public AccountController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var res = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (res == Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }
    }
}