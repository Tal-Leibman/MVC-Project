using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System;
using System.Linq;
using System.Threading;

namespace MVC_Project.Data
{
    public class StoreDataContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> ProductImages { get; set; }

        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasOne(product => product.Seller)
            .WithMany(seller => seller.ProductsSold)
            .HasForeignKey(product => product.SellerId);

            modelBuilder.Entity<Product>()
            .HasOne(product => product.Buyer)
            .WithMany(buyer => buyer.ProductsBought)
            .HasForeignKey(product => product.BuyerId);

            modelBuilder.Entity<Image>()
                .HasOne(image => image.Product)
                .WithMany(product => product.Images);
        }

        public void CheckReservedProducts(TimeSpan timeout)
        {
            var productsToClear = Products
                .Where(p => p.State == Product.States.Reserved)
                .Where(p => DateTime.Now.Subtract(p.LastInteraction) > timeout);
            foreach (Product product in productsToClear)
            {
                product.State = Product.States.Available;
            }
        }
    }
}
