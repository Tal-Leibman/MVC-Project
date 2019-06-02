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
using MVC_Project.Services;
using Newtonsoft.Json;

namespace MVC_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreDataContext dataContext;
        private readonly ICartService cartService;

        public CartController(StoreDataContext dataContext, ICartService cartService)
        {
            this.dataContext = dataContext;
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(long Id)
        {
            return cartService.AddToCart(Id) ? RedirectToAction("Index") : RedirectToAction("Home","Error");
        }
    }
}