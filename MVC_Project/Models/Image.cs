namespace MVC_Project.Models
{
    public class Image
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public byte[] ByteArray { get; set; }
        public Product Product { get; set; }
    }
}
