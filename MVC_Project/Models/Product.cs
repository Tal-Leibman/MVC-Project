using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Product
    {
        public enum States : byte { Available, Reserved, Sold }

        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        [HiddenInput(DisplayValue = false)]
        public States State { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long SellerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long? BuyerId { get; set; }

        public virtual User Seller { get; set; }

        public virtual User Buyer { get; set; }
    }
}
