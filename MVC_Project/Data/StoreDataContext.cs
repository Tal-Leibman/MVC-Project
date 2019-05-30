using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<Product>()
            .HasOne(product => product.Seller)
            .WithMany(seller => seller.ProductsSold)
            .HasForeignKey(product => product.SellerId);

            modelBuilder.Entity<Product>()
            .HasOne(product => product.Buyer)
            .WithMany(buyer => buyer.ProductsBought)
            .HasForeignKey(product => product.BuyerId);
            
            modelBuilder.Entity<ProductImage>()
            .HasOne(image=> image.Product)
            .WithMany(product => product.ProductImages)
            .HasForeignKey(image => image.ProductId);
        }
    }
}
