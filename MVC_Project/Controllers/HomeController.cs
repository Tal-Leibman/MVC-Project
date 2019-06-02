﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVC_Project.Data;
using MVC_Project.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        StoreDataContext _dataContext;
        TimeSpan _resrevedTimeOut;

        public HomeController(StoreDataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _resrevedTimeOut = new TimeSpan(0, 0, config.GetValue<int>("ProductReservedTimeout"));
        }

        public IActionResult Index()
        {
            ViewBag.User = User?.Identity?.Name;

            _dataContext.CheckReservedProducts(_resrevedTimeOut);
            _dataContext.SaveChanges();
            return View(_dataContext.Products.Where(p => p.State == Product.States.Available).ToList());
        }

        public IActionResult ProductDetails(long id)
        {
            Product product = _dataContext.Products.Find(id);
            return product != null ? View(product) : (IActionResult)RedirectToAction("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
