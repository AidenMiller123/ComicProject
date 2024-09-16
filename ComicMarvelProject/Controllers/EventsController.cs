using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class EventsController : Controller
    {
        private readonly Marvel _marvel;

        public EventsController(Marvel marvel)
        {
            _marvel = marvel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("EventsList")]
        public async Task<IActionResult> EventsList(string eventTitle = null)
        {
            EventDataWrapper EventModel = await _marvel.GetEvents(nameStartsWith: eventTitle);
            List<Event> Events = EventModel.Data.Results;

            return View(Events);
        }
    }
}
