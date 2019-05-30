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
            .HasOne(p => p.Seller)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.SellerId);

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Buyer)
            .WithMany(b => b.Products2)
            .HasForeignKey(p => p.BuyerId);
        }
    }
}
