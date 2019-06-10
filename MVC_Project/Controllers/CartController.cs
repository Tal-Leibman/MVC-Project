using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using MVC_Project.Services;
using System.Linq;

namespace MVC_Project.Controllers
{
    public class CartController : Controller
    {
        ICartService _cartService;
        IProductRepository _productRepository;

        public CartController(ICartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            ViewBag.SelectedNavigation = "cart-index-nav";

            Cart cart = _cartService.GetCart();

            var products = _productRepository
                .GetProductList(includeImages: true, includerSeller: true, getAvailable: true, getReserved: true)
                .Where(p => cart.ProductIds.Contains(p.Id))
                .ToList();

            ViewData["cartTotal"] = products.Sum(p => p.Price);
            ViewData["memberDiscount"] = (decimal)0.1;
            return View(products);
        }

        public IActionResult CheckOut()
        {
            _cartService.CheckOut();
            return RedirectToAction("Index");
        } 

        public IActionResult Add(long Id)
            => _cartService.AddToCart(Id) ? RedirectToAction("Index") : RedirectToAction("Home", "Error");

        public IActionResult Remove(long Id)
            => _cartService.RemoveProduct(Id) ? RedirectToAction("Index") : RedirectToAction("Home", "Error");
    }
}