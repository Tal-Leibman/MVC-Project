using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class Cart
    {
        public List<long> ProductIds { get; set; }
        public string UserId { get; set; }
    }
}
