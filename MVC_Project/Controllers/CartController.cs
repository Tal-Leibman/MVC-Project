using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreDataContext dataContext;
        private readonly SignInManager<User> signInManager;

        public CartController(StoreDataContext dataContext, SignInManager<User> signInManager)
        {
            this.dataContext = dataContext;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(long Id)
        {
            Product product = dataContext
                .Products
                .FirstOrDefault(p => p.Id == Id && p.State == Product.States.Available);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            product.State = Product.States.Reserved;
            dataContext.SaveChanges();


            if (User.Identity.IsAuthenticated)
            {
                User loggedInUser = dataContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                HttpContext.Response.Cookies.Append(product.Id.ToString() , loggedInUser.Id);
            }
            else
            {
                HttpContext.Response.Cookies.Append(product.Id.ToString(), "Anon");
            }
            return RedirectToAction("Index");
        }
    }
}