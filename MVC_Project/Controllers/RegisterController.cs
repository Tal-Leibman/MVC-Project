using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class RegisterController : Controller
    {
        private StoreDataContext _dataContext;
        private UserManager<User> _userManager;

        public RegisterController(StoreDataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        { return View(); }

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

            if (res == IdentityResult.Success)
                return RedirectToAction("Index", "Login");

            return RedirectToAction("Error", "Home");
        }
    }
}