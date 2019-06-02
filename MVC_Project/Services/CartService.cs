using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVC_Project.Data;
using MVC_Project.Models;
using Newtonsoft.Json;

namespace MVC_Project.Services
{
    public class CartService : ICartService
    {
        private readonly StoreDataContext _dataContext;
        private readonly IHttpContextAccessor _httpContext;

        public CartService(StoreDataContext dataContext, IHttpContextAccessor httpContext)
        {
            _dataContext = dataContext;
            _httpContext = httpContext;
        }

        public bool AddToCart(long id)
        {
            var product = _dataContext.Products.Find(id);
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
        public void ClearAllProducts() => throw new NotImplementedException();
        public bool RemoveProduct(int id) => throw new NotImplementedException();

        private void SaveCookie(Cart cart)
        {   
            _httpContext.HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
        }

        private Cart ParseCookie()
        {
            var cartJson = _httpContext.HttpContext.Request.Cookies["Cart"];
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
