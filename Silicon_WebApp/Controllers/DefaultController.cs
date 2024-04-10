using Microsoft.AspNetCore.Mvc;

namespace Silicon_WebApp.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
