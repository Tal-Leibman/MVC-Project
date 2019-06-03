using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class ProductAddition
    {
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
