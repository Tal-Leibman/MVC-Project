using Microsoft.AspNetCore.Mvc;
using MVC_Project.Services;

namespace MVC_Project.Controllers
{
    public class InfoController : Controller
    {
        IFaqGetter _faqGetter;

        public InfoController(IFaqGetter faq)
        { _faqGetter = faq; }

        public IActionResult Index() => RedirectToAction("About");
        public IActionResult About() => View();
        public IActionResult Faq() => View(_faqGetter.GetFaq());
    }
}