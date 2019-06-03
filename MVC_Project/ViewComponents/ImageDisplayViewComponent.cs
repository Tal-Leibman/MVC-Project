using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.ViewComponents
{
    public class ImageDisplayViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Image image) => Task.FromResult<IViewComponentResult>(View("Default", image));
    }
}
