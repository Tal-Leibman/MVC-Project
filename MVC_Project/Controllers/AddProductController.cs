using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Helpers;
using MVC_Project.Models;
using MVC_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project.Controllers
{
    public class AddProductController : Controller
    {
        StoreDataContext _dataContext;
        IUserRepository _userRepository;
        IImageConverter _imageConverter;

        public AddProductController(StoreDataContext dataContext, IUserRepository userRepository, IImageConverter imageConverter)
        {
            _dataContext = dataContext;
            _userRepository = userRepository;
            _imageConverter = imageConverter;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Login");

            ViewBag.SelectedNavigation = "addProduct-index-nav";
            return View(new ProductAddition());
        }

        [HttpPost]
        public IActionResult Index(ProductAddition product)
        {
            if (!product.Validate())
                return View();

            Product newProduct = new Product
            {
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

            if (product.Images == null || product.Images.Count < 1)
                newProduct.Images = null;

            else
            {
                List<Image> imageList = new List<Image>();
                foreach (var formFile in product.Images)
                {
                    Image generated;
                    bool success = _imageConverter.ToDatabaseImage(formFile, out generated);

                    if (success)
                    {
                        generated.Product = newProduct;
                        imageList.Add(generated);
                    }
                }

                newProduct.Images = imageList;
            }

            _dataContext.Products.Add(newProduct);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}