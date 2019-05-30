﻿using Microsoft.AspNetCore.Mvc;
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

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PasswordHash { get; set; }

        public List<Product> Products { get; set; }
        public List<Product> Products2 { get; set; }

    }
}
