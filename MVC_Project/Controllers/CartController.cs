﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models;
using MVC_Project.Services;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly StoreDataContext _dataContext;

        public CartController(ICartService cartService, StoreDataContext dataContext)
        {
            _cartService = cartService;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            Cart cart = _cartService.GetCart();
            var products =_dataContext.Products
                .Include(p => p.Images)
                .Include(p => p.Seller)
                .Where(product => cart.ProductIds.Contains(product.Id))
                .ToList();
            return View(products);
        }
        public IActionResult Add(long Id)
        {
            return _cartService.AddToCart(Id) ? RedirectToAction("Index") : RedirectToAction("Home", "Error");
        }
    }
}