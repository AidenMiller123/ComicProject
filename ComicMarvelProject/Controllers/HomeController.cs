using ComicMarvelProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hero()
        {
            return View();
        }
    }
}