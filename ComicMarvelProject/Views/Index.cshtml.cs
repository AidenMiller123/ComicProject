using ComicMarvelProject.Models;
using ComicMarvelProject.Services.MarvelApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComicMarvelProject.Views
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public CharacterComicViewModel CharacterViewModel { get; set; }
        public async Task OnGetAsync(string searchString = "Thor")
        {
            try
            {
                var marvelApi = new Marvel();
                CharactersDataWrapper characterData = await marvelApi.GetCharcters(searchString);
                Character character = characterData.Data.Results.FirstOrDefault();

                CharacterViewModel = new CharacterComicViewModel
                {
                    Character = character,
                    AttruibutionText = characterData.Attributiontext
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching hero data.");
                // Optionally handle errors here
            }
        }
    }
}
