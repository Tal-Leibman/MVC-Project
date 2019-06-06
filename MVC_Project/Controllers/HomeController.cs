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
            _resrevedTimeOut = new TimeSpan(0, 0, config.GetValue<int>("ProductReservedTimeout"));
            _dataContext = dataContext;
        }

        public IActionResult Index(string sortOrder)
        {
            ViewBag.User = User?.Identity?.Name;
            ViewBag.SelectedNavigation = "home-index-nav";

            _dataContext.CheckReservedProducts(_resrevedTimeOut);
            _dataContext.SaveChanges();

            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var postList =
                _dataContext
                .Products
                .Include(p => p.Seller)
                .Include(p => p.Images)
                .Where(p => p.State == Product.States.Available);

            switch (sortOrder)
            {
                case "Title":
                    postList = postList.OrderByDescending(p => p.Title);
                    break;
                case "Date":
                    postList = postList.OrderBy(p => p.Date);
                    break;
                case "Price":
                    postList = postList.OrderByDescending(p => p.Price);
                    break;
                default:
                    postList = postList.OrderBy(p => p.Id);
                    break;
            }

            return View(postList.ToList());
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
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
