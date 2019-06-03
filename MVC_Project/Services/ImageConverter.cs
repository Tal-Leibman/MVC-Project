using Microsoft.AspNetCore.Http;
using MVC_Project.Models;
using System.IO;

namespace MVC_Project.Services
{
    public interface IImageConverter
    {
        bool ToDatabaseImage(IFormFile formFile, out Image image);
    }

    public class ImageConverter : IImageConverter
    {
        public bool ToDatabaseImage(IFormFile formFile, out Image image)
        {
            image = default;
            if (formFile == null)
                return false;

            using (var ms = new MemoryStream())
            {
                formFile.OpenReadStream().CopyTo(ms);
                image = new Image { ByteArray = ms.ToArray() };
            };

            return true;
        }
    }
}
