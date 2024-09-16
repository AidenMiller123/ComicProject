using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class CreatorsController : Controller
    {
        private readonly Marvel _marvel;

        public CreatorsController(Marvel marvel)
        {
            _marvel = marvel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("CreatorsList")]
        public async Task<IActionResult> CreatorsList(string firstName = null)
        {
            CreatorDataWrapper CreatorModel = await _marvel.GetCreators(firstName: firstName);
            List<Creator> Creators = CreatorModel.Data.Results;

            return View(Creators);
        }
    }
}
