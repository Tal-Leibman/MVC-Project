using Microsoft.AspNetCore.Http;
using MVC_Project.Data;
using MVC_Project.Models;
using Newtonsoft.Json;
using System;

namespace MVC_Project.Services
{
    public class CartService : ICartService
    {
        private readonly StoreDataContext _dataContext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly string _cartCookieKey;

        public CartService(StoreDataContext dataContext, IHttpContextAccessor httpContext)
        {
            _dataContext = dataContext;
            _httpContext = httpContext;
            _cartCookieKey = "Cart";
        }

        public bool AddToCart(long id)
        {
            Product product = _dataContext.Products.Find(id);
            if (product?.State != Product.States.Available)
                return false;
            Cart cart = ParseCookie();
            cart.ProductIds.Add(id);
            product.State = Product.States.Reserved;
            product.LastInteraction = DateTime.Now;
            _dataContext.SaveChanges();
            SaveCookie(cart);
            return true;
        }
        public void ClearAllProducts()
        {
            _httpContext.HttpContext.Response.Cookies.Delete(_cartCookieKey);
        }
        public bool RemoveProduct(int id) => throw new NotImplementedException();

        private void SaveCookie(Cart cart)
        {
            _httpContext.HttpContext.Response.Cookies.Append(_cartCookieKey, JsonConvert.SerializeObject(cart));
        }

        private Cart ParseCookie()
        {
            string cartJson = _httpContext.HttpContext.Request.Cookies[_cartCookieKey];
            Cart cart = null;
            try
            {
                cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            }
            catch
            {
                cart = new Cart();
            }
            return cart;
        }
    }
}
