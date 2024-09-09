using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Charcters()
        {
            return View();
        }

        public async Task<IActionResult> Characters(string characterName = null)
        {
            var _marvel = new Marvel();
            CharactersDataWrapper CharacterModel = await _marvel.GetCharcters(NameStartsWith: characterName);
            List<Character> Characters = CharacterModel.Data.Results;
            
            
            return View(Characters);
        }
    }
}