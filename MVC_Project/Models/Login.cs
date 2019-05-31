using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
