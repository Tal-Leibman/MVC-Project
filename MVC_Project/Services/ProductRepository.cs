using MVC_Project.Data;
using MVC_Project.Models;
using System.Collections.Generic;
using System.Linq;

//GetContents
//RemoveProduct
//AddProduct
//ValidateContent
//ReaddContentFromCookie

namespace MVC_Project.Services
{
    public interface IProductRepository
    {
        List<Product> GetProductList();
        Product GetProduct(long id);
        bool AddProduct(Product product);
        void UpdateState(long id, Product.States state);
    }

    public class ProductRepository : IProductRepository
    {
        private StoreDataContext _context;

        public ProductRepository(StoreDataContext ctx) => _context = ctx;

        public List<Product> GetProductList() => _context.Products.ToList();
        public Product GetProduct(long id) => _context.Products.FirstOrDefault(p => p.Id == id);
        public void UpdateState(long id, Product.States state) => GetProduct(id).State = state;


        public bool AddProduct(Product product)
        {
            if (!ValidateProduct(product))
                return false;

            _context.Products.Add(product);
            return _context.SaveChanges() > 0;
        }

        bool ValidateProduct(Product product)
        {
            return product != null
                && !string.IsNullOrEmpty(product.Title)
                && !string.IsNullOrEmpty(product.ShortDescription)
                && !string.IsNullOrEmpty(product.LongDescription)
                && product.Price > 0;
        }
    }
}
