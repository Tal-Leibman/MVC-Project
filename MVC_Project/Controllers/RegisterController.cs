using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class RegisterController : Controller
    {
        private StoreDataContext dataContext;
        private UserManager<User> userManager;
        public RegisterController(StoreDataContext dataContext, UserManager<User> userManager)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

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
            IdentityResult res = await userManager.CreateAsync(newUser, registerUser.Password);
            if (res == IdentityResult.Success)
            {
                return RedirectToAction("Index", "Account");
            }
            return RedirectToAction("Error", "Home");
        }
    }
}