using Microsoft.AspNetCore.Mvc;
using MVC_Project.Services;

namespace MVC_Project.Controllers
{
    public class InfoController : Controller
    {
        private IFaqGetter _faqGetter;
        public InfoController(IFaqGetter faq) => _faqGetter = faq;

        public IActionResult Index() => RedirectToAction("About");

        public IActionResult About()
        {
            ViewBag.SelectedNavigation = "info-about-nav";
            return View();
        }

        public IActionResult Faq()
        {
            ViewBag.SelectedNavigation = "info-faq-nav";
            return View(_faqGetter.GetFaq());
        }
    }
}