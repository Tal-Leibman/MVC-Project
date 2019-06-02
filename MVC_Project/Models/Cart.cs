using System.Collections.Generic;

namespace MVC_Project.Models
{
    public class Cart
    {
        public HashSet<long> ProductIds { get; set; }
        public Cart() => ProductIds = new HashSet<long>();
    }
}
