using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Models
{
    public class ProductImage
    {
        public long ProductImageId { get; set; }
        public string FileName { get; set; }
        public byte[] ByteArray { get; set; }
        public long ProductId { get; set; }
    }
}
