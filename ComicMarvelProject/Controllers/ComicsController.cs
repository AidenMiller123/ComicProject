using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class ComicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Comics()
        {
            return View("Index");
        }
    }
}
