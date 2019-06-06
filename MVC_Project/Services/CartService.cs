using Microsoft.AspNetCore.Http;
using MVC_Project.Data;
using MVC_Project.Models;
using Newtonsoft.Json;
using System;

namespace MVC_Project.Services
{
    public interface ICartService
    {
        Cart GetCart();
        bool RemoveProduct(int id);
        void ClearAllProducts();
        bool AddToCart(long id);
    }

    public class CartService : ICartService
    {
        readonly StoreDataContext _dataContext;
        readonly IHttpContextAccessor _httpContext;
        readonly string _cartCookieKey;

        public CartService(StoreDataContext dataContext, IHttpContextAccessor httpContext)
        {
            _dataContext = dataContext;
            _httpContext = httpContext;
            _cartCookieKey = "Cart";
        }

        public Cart GetCart() => ParseCookie();

        public void ClearAllProducts() =>
            _httpContext.HttpContext.Response.Cookies.Delete(_cartCookieKey);

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

        public bool RemoveProduct(int id)
        {
            Product product = _dataContext.Products.Find(id);
            if (product == null)
                return false;

            Cart cart = ParseCookie();
            if (cart.ProductIds.Remove(id))
            {
                product.LastInteraction = DateTime.Now;
                product.State = product.State == Product.States.Reserved ? Product.States.Available : product.State;
                _dataContext.SaveChanges();
                SaveCookie(cart);
                return true;
            }

            return false;
        }

        Cart ParseCookie()
        {
            string cartJson = _httpContext.HttpContext.Request.Cookies[_cartCookieKey];
            Cart cart = null;

            try
            { cart = JsonConvert.DeserializeObject<Cart>(cartJson); }
            catch
            { cart = new Cart(); }

            return cart;
        }

        void SaveCookie(Cart cart)
             => _httpContext.HttpContext.Response.Cookies.Append(_cartCookieKey, JsonConvert.SerializeObject(cart));
    }
}
