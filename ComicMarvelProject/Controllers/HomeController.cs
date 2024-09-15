using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;

namespace ComicMarvelProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly Marvel _marvel;
        public HomeController(Marvel marvel)
        {
            _marvel = marvel;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Character(int characterId)
        {
            CharactersDataWrapper CharacterModel = await _marvel.GetCharacter(characterId);
            Character Character = CharacterModel.Data.Results[0];

            return View(Character);
        }

        public IActionResult Characters()
        {
            return View();
        }

        [HttpGet("CharactersList")]
        public async Task<IActionResult> CharactersList(string characterName = null)
        {
            CharactersDataWrapper CharacterModel = await _marvel.GetCharcters(NameStartsWith: characterName);
            List<Character> Characters = CharacterModel.Data.Results;
            
            
            return View(Characters);
        }

        [HttpGet("Comic/{characterId}")]
        public async Task<IActionResult> ComicDetails(int characterId)
        {
			var characterModel = await _marvel.GetCharacter(characterId);
			if (characterModel == null || characterModel.Data.Results.Count == 0)
			{
				return NotFound();
			}

			var character = characterModel.Data.Results[0];
			var comicData = await _marvel.GetComicForCharacter(characterId);
			if (comicData == null || comicData.Data.Results.Count == 0)
			{
				return NotFound();
			}

            var viewModel = new CharacterComicViewModel
			{
				Character = character,
				Comics = comicData.Data.Results,
				AttruibutionText = comicData.AttributionText
			};

            return View(viewModel);
		}
    }
}