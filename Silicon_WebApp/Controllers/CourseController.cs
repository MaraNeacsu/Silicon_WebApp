using Microsoft.AspNetCore.Mvc;

namespace Silicon_WebApp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
