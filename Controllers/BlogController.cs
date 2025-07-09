using Microsoft.AspNetCore.Mvc;

namespace BlogBuilder.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
