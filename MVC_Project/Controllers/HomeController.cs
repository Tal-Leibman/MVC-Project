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


        public IActionResult Index()
        {
            ViewBag.SelectedNavigation = "home-index-nav";
            _dataContext.CheckReservedProducts(_resrevedTimeOut);
            _dataContext.SaveChanges();
            IQueryable<Product> postList =
                _dataContext
                .Products
                .Include(p => p.Seller)
                .Include(p => p.Images)
                .Where(p => p.State == Product.States.Available);

            bool isSort = HttpContext.Request.Query.TryGetValue("sort", out var sortType);
            if (isSort)
            {
                postList = SortProducts(postList, sortType);
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



        private IQueryable<Product> SortProducts(IQueryable<Product> postList, string sortType)
        {
            bool isDescending = HttpContext.Request.Query.TryGetValue("descending", out var asec);
            if (isDescending)
            {
                switch (sortType)
                {

                    case "Date": return postList.OrderByDescending(p => p.Date);
                    case "Title": return postList.OrderByDescending(p => p.Title);
                    case "Price": return postList.OrderByDescending(p => p.Price);
                    default:
                        break;
                }
            }
            else
            {
                switch (sortType)
                {
                    case "Date": return postList.OrderBy(p => p.Date);
                    case "Title": return postList.OrderBy(p => p.Title);
                    case "Price": return postList.OrderBy(p => p.Price);
                    default:
                        break;
                }
            }
            return null;
        }
    }
}




