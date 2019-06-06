using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class RegisterController : Controller
    {
        UserManager<User> _userManager;

        public RegisterController(UserManager<User> userManager) => _userManager = userManager;

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            var newUser = new User()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                BirthDate = registerUser.BirthDate,
            };

            IdentityResult res = await _userManager.CreateAsync(newUser, registerUser.Password);
            return res == IdentityResult.Success ? RedirectToAction("Index", "Login") : RedirectToAction("Error", "Home");
        }
    }
}