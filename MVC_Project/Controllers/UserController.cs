using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Helpers;
using MVC_Project.Models;
using MVC_Project.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class UserController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        IUserRepository _userRepo;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, IUserRepository userRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;
        }


        public IActionResult Index() => View();


        // ----- Logging in/out

        public IActionResult LogIn(bool failedLogin = false)
        {
            ViewBag.SelectedNavigation = "login-index-nav";
            ViewBag.BadLogin = failedLogin;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Login login)
        {
            var res = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true, false);
            return res == Microsoft.AspNetCore.Identity.SignInResult.Success ? RedirectToAction("Index", "Home") : LogIn(true);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete("AspNetCore.Identity.Application");
            return RedirectToAction("Index", "Home");
        }

        // ----- Registering

        public IActionResult Register(List<IdentityError> errors = null)
        {
            ViewBag.SelectedNavigation = "register-index-nav";
            ViewData["errors"] = errors;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (!registerUser.Validate())
            {
                var errorlist = new List<IdentityError>(1)
                {
                    new IdentityError { Description = "Invalid User input for registration" }
                };
                return Register(errors: errorlist);
            }

            var newUser = new User()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                BirthDate = registerUser.BirthDate,
            };
            IdentityResult res = await _userManager.CreateAsync(newUser, registerUser.Password);
            if (res.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return Register(res.Errors.ToList());
        }

        // ----- Edit details

        public IActionResult UpdateDetails(List<IdentityError> errors = null)
        {
            if (!User.Identity.IsAuthenticated)
                return LogIn();

            ViewData["errors"] = errors;

            User user = _userRepo.GetUserList.FirstOrDefault(u => u.UserName == User.Identity.Name);

            return View(new UpdateUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetails(UpdateUser newDetails)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LogIn();
            }

            User user = _userRepo.GetUserList.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
            {
                return LogIn();
            }
            IdentityResult result = null;
            List<IdentityError> errors = null;
            if (!string.IsNullOrEmpty(newDetails.CurrentPassword))
            {
                result = await _userManager.ChangePasswordAsync(user, newDetails.CurrentPassword, newDetails.Password);
                errors = result.Errors.ToList();
            }
            _userRepo.UpdateUser(user.Id, newDetails);
            return UpdateDetails(errors);
        }
    }
}