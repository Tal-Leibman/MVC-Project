using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class Product
    {
        enum States : byte { Available, Reserved, Sold }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public long ID { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public long OwnerID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long UserID { get; set; }

        [Required]
        public string Title { get; set; }

        [StringLength(50)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [StringLength(50)]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public byte[][] Images { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public byte State { get; set; }
    }
}
