﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class LoginController : Controller
    {
        private SignInManager<User> _signInManager;

        public LoginController(SignInManager<User> signInManager)
        { _signInManager = signInManager; }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            var res = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (res == Microsoft.AspNetCore.Identity.SignInResult.Success)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}