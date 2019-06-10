using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVC_Project.Data;
using MVC_Project.Models;
using MVC_Project.Services;
using System;
using System.Linq;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        TimeSpan _resrevedTimeOut;
        IUserRepository _userRepo;
        StoreDataContext _dataContext;
        IProductRepository _productRepository;

        public HomeController(StoreDataContext dataContext, IProductRepository productRepository, IConfiguration config, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _dataContext = dataContext;
            _productRepository = productRepository;
            _resrevedTimeOut = new TimeSpan(0, 0, config.GetValue<int>("ProductReservedTimeout"));
        }

        public IActionResult Index()
        {
            ViewBag.SelectedNavigation = "home-index-nav";
            _dataContext.CheckReservedProducts(_resrevedTimeOut);

            IQueryable<Product> postList = _productRepository.GetProductList(includeImages: true, includerSeller: true, getAvailable: true);

            bool isSort = HttpContext.Request.Query.TryGetValue("sort", out var sortType);
            if (isSort)
                postList = SortProducts(postList, sortType);

            return View(postList.ToList());
        }

        public IActionResult ProductDetails(long id)
        {
            Product product = _productRepository.GetProduct(id, includeImages: true, includerSeller: true, getAvailable: true);
            return product == default ? View("ProductError") : View(product);
        }

        public IActionResult RemoveProduct(long id)
        {
            Product product = _productRepository.GetProduct(id);
            if (product != null)
            {
                User seller = _userRepo.GetUser(product.SellerId);
                if (seller != null && User.Identity.Name == seller.UserName)
                    if (_productRepository.RemoveProduct(id))
                        return RedirectToAction("Index");
            }
            return View("ProductError");
        }

        public IActionResult Error() => View();

        IQueryable<Product> SortProducts(IQueryable<Product> postList, string sortType)
        {
            bool isDescending = HttpContext.Request.Query.TryGetValue("descending", out var asec);
            if (isDescending)
            {
                switch (sortType)
                {
                    case "Date": return postList.OrderByDescending(p => p.Date);
                    case "Title": return postList.OrderByDescending(p => p.Title);
                    case "Price": return postList.OrderByDescending(p => p.Price);
                    default: break;
                }
            }
            else
            {
                switch (sortType)
                {
                    case "Date": return postList.OrderBy(p => p.Date);
                    case "Title": return postList.OrderBy(p => p.Title);
                    case "Price": return postList.OrderBy(p => p.Price);
                    default: break;
                }
            }
            return null;
        }

    }
}