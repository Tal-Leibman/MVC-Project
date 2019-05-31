using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class RegisterUser
    {
        [Display(Name = "User name")]
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(60)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "E-Mail")]
        [MaxLength(254)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
