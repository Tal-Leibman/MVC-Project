﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class Product
    {
        public enum States : byte { Available, Reserved, Sold, Removed }

        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public DateTime LastInteraction { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public ICollection<Image> Images { get; set; }

        [HiddenInput(DisplayValue = false)]
        public States State { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SellerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string BuyerId { get; set; }

        public User Seller { get; set; }

        public User Buyer { get; set; }
    }
}
