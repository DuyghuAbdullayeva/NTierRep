using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCourseNTier.MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
