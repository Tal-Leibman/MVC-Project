﻿using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class AddProductController : Controller
    {
        StoreDataContext _dataContext;

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Index(Product product)
        {
            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}