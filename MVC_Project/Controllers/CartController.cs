using Microsoft.AspNetCore.Mvc;
using MVC_Project.Services;

namespace MVC_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;
        public IActionResult Index() => View();
        public IActionResult Add(long Id)
        {
            return _cartService.AddToCart(Id) ? RedirectToAction("Index", "Home") : RedirectToAction("Home", "Error");
        }
    }
}