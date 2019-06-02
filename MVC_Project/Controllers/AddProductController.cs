using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;
using MVC_Project.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Controllers
{
    public class AddProductController : Controller
    {
        StoreDataContext _dataContext;
        IUserRepository _userRepository;

        public AddProductController(StoreDataContext dataContext, IUserRepository userRepository)
        {
            _dataContext = dataContext;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View(new ProductAddition());

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Index(ProductAddition product)
        {
            if (!ValidateProduct(product))
                return View();

            byte[] image;
            using (var ms = new MemoryStream())
            {
                product.Image.OpenReadStream().CopyTo(ms);
                image = ms.ToArray();
            };

            Product newProduct = new Product
            {
                Id = 1,
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                Date = DateTime.Now,
                LastInteraction = DateTime.Now,
                Price = product.Price,
                Images = null,
                State = Product.States.Available,
                SellerId = _userRepository.GetUserList().FirstOrDefault(u => u.UserName.Equals(User.Identity.Name)).Id,
                BuyerId = null,
                Seller = _userRepository.GetUserList().FirstOrDefault(u => u.UserName.Equals(User.Identity.Name)),
                Buyer = null
            };

            _dataContext.Products.Add(newProduct);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }

        bool ValidateProduct(ProductAddition product)
        {
            if (product == null
                || string.IsNullOrEmpty(product.Title)
                || string.IsNullOrEmpty(product.ShortDescription)
                || string.IsNullOrEmpty(product.LongDescription)
                || product.Price <= 0)
                return false;
            return true;
        }
    }
}