using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCourseNTier.MVC.Controllers
{
    public class CacheController : Controller
    {
        public IActionResult Index()
        {
            string[] adSoyadlar = { "Xumar Shiraliyeva", "Duygu Abdullayeva" };
            return View(adSoyadlar);
        }
    }
}
