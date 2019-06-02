using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.ViewComponents
{
    public class ImageDisplayViewcomponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Image product) => Task.FromResult<IViewComponentResult>(View("Default", product));
    }
}
