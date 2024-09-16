using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class SeriesController : Controller
    {
        private readonly Marvel _marvel;

        public SeriesController(Marvel marvel)
        {
            _marvel = marvel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("SeriesList")]
        public async Task<IActionResult> SeriesList(string seriesTitle = null)
        {
            SeriesDataWrapper SeriesModel = await _marvel.GetSeries(titleStartsWith: seriesTitle);
            List<Series> Series = SeriesModel.Data.Results;

            return View(Series);
        }
    }
}
