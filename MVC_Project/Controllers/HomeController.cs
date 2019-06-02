using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private StoreDataContext dataContext;
        private TimeSpan resrevedTimeOut;

        public HomeController(StoreDataContext dataContext)
        {
            this.dataContext = dataContext;
            resrevedTimeOut = new TimeSpan(0, 0, 2);
        }

        public IActionResult Index()
        {
            ViewBag.User = User?.Identity?.Name;

            dataContext.CheckReservedProducts(resrevedTimeOut);
            dataContext.SaveChanges();
            return View(
                dataContext
                .Products
                .Include(p=> p.Seller)
                .Include(p=> p.Images)
                .Where(p => p.State == Product.States.Available)
                .ToList()
                );
        }

        public IActionResult ProductDetails(long id)
        {
            Product product = dataContext.Products.Find(id);
            return product != null ? View(product) : (IActionResult)RedirectToAction("Error");
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            dataContext.Products.Add(product);
            dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
