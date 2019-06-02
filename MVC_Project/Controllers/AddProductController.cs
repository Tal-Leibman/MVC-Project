using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class AddProductController : Controller
    {
        StoreDataContext _dataContext;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromBody]Product product)
        {

            return View();
        }
    }
}