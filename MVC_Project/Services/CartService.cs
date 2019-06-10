using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MVC_Project.Services
{
    public interface ICartService
    {
        Cart GetCart();
        bool RemoveProduct(long id);
        void ClearAllProducts();
        bool AddToCart(long id);
        void CheckOut();
    }

    public class CartService : ICartService
    {
        StoreDataContext _dataContext;
        IHttpContextAccessor _httpContext;
        IProductRepository _productRepo;
        string _anonymousUser;

        public CartService(StoreDataContext dataContext, IHttpContextAccessor httpContext, IProductRepository productRepo)
        {
            _dataContext = dataContext;
            _httpContext = httpContext;
            _productRepo = productRepo;
            _anonymousUser = "anonymous";
        }

        public Cart GetCart() => ParseCookie();

        public void ClearAllProducts()
        {
            Cart cart = GetCart();
            _httpContext.HttpContext.Response.Cookies.Delete(cart.UserName);
        }

        public bool AddToCart(long id)
        {
            Product product = _productRepo.GetProduct(id);
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

        public bool RemoveProduct(long id)
        {
            Product product = _productRepo.GetProduct(id);
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
            string userName = _httpContext.HttpContext.User.Identity.IsAuthenticated ? _httpContext.HttpContext.User.Identity.Name : _anonymousUser;
            string cartJson = _httpContext.HttpContext.Request.Cookies[userName];
            Cart cart = null;

            try
            {
                cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            }
            catch
            {
                cart = new Cart()
                { UserName = userName };
            }

            return cart;
        }

        void SaveCookie(Cart cart)
             => _httpContext.HttpContext.Response.Cookies.Append(cart.UserName, JsonConvert.SerializeObject(cart));

        public void CheckOut()
        {
            Cart cart = GetCart();
            User buyer = _dataContext.Users.FirstOrDefault(u => u.UserName == cart.UserName);

            _dataContext
                .Products
                .Where(p => cart.ProductIds.Contains(p.Id))
                .Where(p => p.State != Product.States.Sold)
                .Where(p => p.State != Product.States.Removed)
                .ToList()
                .ForEach(p =>
                {
                    p.State = Product.States.Sold;
                    p.BuyerId = buyer.Id;
                });
            _dataContext.SaveChanges();
            ClearAllProducts();
        }
    }
}
