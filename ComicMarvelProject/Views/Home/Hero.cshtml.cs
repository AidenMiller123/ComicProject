using ComicMarvelProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ComicMarvelProject.Views.Home
{
    public class HeroModel : PageModel
    {
        private readonly ILogger<HeroModel> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public CharacterComicViewModel CharacterViewModel { get; set; }

        public HeroModel(ILogger<HeroModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync(string searchString = "Thor")
        {
            try
            {
                var marvelApi = new MarvelAPI.Marvel();
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
