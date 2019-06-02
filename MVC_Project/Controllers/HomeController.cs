using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private StoreDataContext _dataContext;
        private TimeSpan _resrevedTimeOut;

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
            return View(
                _dataContext
                .Products
                .Include(p => p.Seller)
                .Include(p => p.Images)
                .Where(p => p.State == Product.States.Available)
                .ToList()
                );
        }

        public IActionResult ProductDetails(long id)
        {
            Product product = _dataContext
                   .Products
                   .Include(p => p.Seller)
                   .Include(p => p.Images)
                   .FirstOrDefault(p => p.Id == id);
            return product != null ? View(product) : (IActionResult)RedirectToAction("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
