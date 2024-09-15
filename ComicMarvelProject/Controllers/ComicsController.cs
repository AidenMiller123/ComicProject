using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class ComicsController : Controller
    {
        private readonly Marvel _marvel;

        public ComicsController(Marvel marvel)
		{
			_marvel = marvel;
		}

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ComicsList")]
        public async Task<IActionResult> ComicsList(string comicTitle = null)
        {
            ComicDataWrapper ComicModel = await _marvel.GetComics(titleStartsWith: comicTitle);
            List<Comic> Comics = ComicModel.Data.Results;

            return View(Comics);
        }

        [HttpGet("Comics/Comic/{id}")]
        public async Task<IActionResult> Comic(int id)
        {
            ComicDataWrapper ComicModel = await _marvel.GetComic(id);
            Comic Comic = ComicModel.Data.Results[0];

            return View(Comic);
        }
    }
}
