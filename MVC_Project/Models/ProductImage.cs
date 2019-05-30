using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class ProductImage
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public byte[] ByteArray { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
