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
using Newtonsoft.Json;

namespace MVC_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreDataContext dataContext;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartController(StoreDataContext dataContext, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.dataContext = dataContext;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
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
            string cartJson = Request.Cookies["Cart"];
            Cart cart = null;
            try
            {
                cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            }
            catch
            {
                cart = new Cart();
            }
            cart.ProductIds.Add(product.Id);
            Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index");
        }
    }
}