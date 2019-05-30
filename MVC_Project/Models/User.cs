using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class User
    {
        public User()
        {
                
        }
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Username { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PasswordHash { get; set; }
        public List<Product> ProductsSold { get; set; }
        public List<Product> Productsbought { get; set; }
    }
}
