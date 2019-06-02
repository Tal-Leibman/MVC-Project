using MVC_Project.Models;
using System.IO;

namespace MVC_Project.Services
{
    public interface IImageConverter
    {
        bool ToImage(string path, out Image image);
    }

    public class ImageConverter : IImageConverter
    {
        public bool ToImage(string path, out Image image)
        {
            image = default;
            if (!File.Exists(path))
                return false;

            image = new Image
            {
                ByteArray = File.ReadAllBytes(path),
                FileName = Path.GetFileName(path),
            };

            return true;
        }
    }
}
