using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class StoriesController : Controller
    {
        private readonly Marvel _marvel;

        public StoriesController(Marvel marvel)
        {
            _marvel = marvel;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("StoriesList")]
        public async Task<IActionResult> StoriesList(DateTime? modifiedSince = null)
        {
            StoryDataWrapper StoryModel = await _marvel.GetStories(modifedSince: modifiedSince);
            List<Story> Stories = StoryModel.Data.Results;

            return View(Stories);
        }
    }
}
