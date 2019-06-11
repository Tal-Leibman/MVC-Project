using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Helpers;
using MVC_Project.Models;
using MVC_Project.Services;

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


        public IActionResult Index()
        {
            return View();
        }


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

        public IActionResult Register(bool userExists = false)
        {
            ViewBag.SelectedNavigation = "register-index-nav";
            ViewBag.UserExists = userExists;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (!registerUser.Validate())
                return Register(false);

            var newUser = new User()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                BirthDate = registerUser.BirthDate,
            };

            IdentityResult res = await _userManager.CreateAsync(newUser, registerUser.Password);
            return res == IdentityResult.Success ? LogIn() : Register(true);
        }

        // ----- Edit details

        public IActionResult UpdateDetails()
        {
            if (!User.Identity.IsAuthenticated)
                return LogIn();

            User model = _userRepo.GetUserList.FirstOrDefault(u => u.UserName == User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateDetails(User newDetails)
        {
            if (!User.Identity.IsAuthenticated)
                return LogIn();

            string userID = _userRepo.GetUserList.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            if (userID == default)
                return LogIn();

            bool success = _userRepo.UpdateUser(userID, newDetails);
            return UpdateDetails();
        }
    }
}