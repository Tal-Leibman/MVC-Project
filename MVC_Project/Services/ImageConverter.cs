using MVC_Project.Models;
using System.IO;

namespace MVC_Project.Services
{
    public interface IImageConverter
    {
        bool ToProductImage(string path, out Image pImage);
    }

    public class ImageConverter : IImageConverter
    {
        public bool ToProductImage(string path, out Image pImage)
        {
            pImage = default;
            if (!File.Exists(path))
                return false;

            pImage = new Image
            {
                ByteArray = File.ReadAllBytes(path),
                FileName = Path.GetFileName(path),
            };

            return true;
        }
    }
}
