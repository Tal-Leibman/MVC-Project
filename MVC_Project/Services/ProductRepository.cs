using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Helpers;
using MVC_Project.Models;
using System.Linq;

namespace MVC_Project.Services
{
    public interface IProductRepository
    {
        Product GetProduct(long id);
        Product GetProduct(long id, bool includeImages = false, bool includeBuyer = false, bool includerSeller = false, bool getAvailable = false, bool getReserved = false, bool getSold = false, bool getRemoved = false);
        IQueryable<Product> GetProductList(int offset = 0, int count = 0, bool includeImages = false, bool includeBuyer = false, bool includerSeller = false, bool getAvailable = false, bool getReserved = false, bool getSold = false, bool getRemoved = false);
        bool AddProduct(Product product);
        bool RemoveProduct(long id);
        bool ValidateProduct(Product product);
        void UpdateState(long id, Product.States state);
    }

    public class ProductRepository : IProductRepository
    {
        StoreDataContext _context;

        public ProductRepository(StoreDataContext ctx) => _context = ctx;

        public Product GetProduct(long id) => _context.Products.FirstOrDefault(p => p.Id == id);
        public void UpdateState(long id, Product.States state) => GetProduct(id).State = state;

        public bool AddProduct(Product product)
        {
            if (!ValidateProduct(product))
                return false;

            _context.Products.Add(product);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveProduct(long id)
        {
            Product product = _context
                   .Products
                   .Where(p => p.State == Product.States.Available || p.State == Product.States.Reserved)
                   ?.FirstOrDefault(p => p.Id == id);

            product.State = Product.States.Removed;
            return _context.SaveChanges() > 0;
        }

        public bool ValidateProduct(Product product) => product.Validate();

        public Product GetProduct(long id, bool includeImages = false, bool includeBuyer = false, bool includerSeller = false, bool getAvailable = false, bool getReserved = false, bool getSold = false, bool getRemoved = false)
        {
            return GetProductList(0, 0, includeImages, includeBuyer, includerSeller, getAvailable, getReserved, getSold)
                .FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Product> GetProductList(int offset = 0, int count = 0, bool includeImages = false, bool includeBuyer = false, bool includerSeller = false, bool getAvailable = false, bool getReserved = false, bool getSold = false, bool getRemoved = false)
        {
            return _context
                .Products
                .Where(p => (getAvailable == true && p.State == Product.States.Available) ||
                            (getReserved == true && p.State == Product.States.Reserved) ||
                            (getRemoved == true && p.State == Product.States.Removed) ||
                            (getSold == true && p.State == Product.States.Sold))
                .If(includeBuyer, p => p.Include(p2 => p2.Buyer))
                .If(includeImages, p => p.Include(p2 => p2.Images))
                .If(includerSeller, p => p.Include(p2 => p2.Seller))
                .Skip(offset)
                .Take(count);
        }
    }
}
