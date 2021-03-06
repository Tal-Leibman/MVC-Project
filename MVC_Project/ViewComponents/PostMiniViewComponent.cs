﻿using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.Threading.Tasks;

namespace MVC_Project.ViewComponents
{
    public class PostMiniViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Product product) => Task.FromResult<IViewComponentResult>(View("Default", product));
    }
}
