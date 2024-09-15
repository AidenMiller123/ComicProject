using ComicMarvelProject.Models;
using MailKit.Search;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace ComicMarvelProject.Services.MarvelApi
{
    public class Marvel
    {
        private const string BASE_URL = "http://gateway.marvel.com/v1/public";
        private readonly string _publicKey;
        private readonly string _privateKey;
        private static HttpClient _client;

        public Marvel(HttpClient client, IConfiguration configuration)
        {
            _client = client;
			_publicKey = configuration["Marvel:PublicKey"];  
			_privateKey = configuration["Marvel:PrivateKey"];
		}

		public async Task<CharactersDataWrapper> GetCharcters(string Name = null,
															string? NameStartsWith = null,
															DateTime? ModifiedSince = null,
															IEnumerable<int>? Comics = null,
															IEnumerable<int>? Series = null,
															IEnumerable<int>? Events = null,
															IEnumerable<int>? Stories = null,
															string? Order = "modified",
                                                            int? Limit = 75,
                                                            int? Offset = null)
        {
            //We need a timestamp
            string timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            //we need use a has to call the marvel api
            string s = string.Format("{0}{1}{2}", timestamp, _privateKey, _publicKey);

            string hash = CreateHash(s);

			//format the url string with search critieria
			string requestURL = $"{BASE_URL}/characters?ts={timestamp}&apikey={_publicKey}&hash={hash}";

			// Add query parameters if they exist
			if (!string.IsNullOrEmpty(Name))
				requestURL += $"&name={Name}";
			if (!string.IsNullOrEmpty(NameStartsWith))
				requestURL += $"&nameStartsWith={NameStartsWith}";
			if (ModifiedSince.HasValue)
				requestURL += $"&modifiedSince={ModifiedSince.Value.ToString("yyyy-MM-dd")}";
			if (Comics != null && Comics.Any())
				requestURL += $"&comics={string.Join(",", Comics)}";
			if (Series != null && Series.Any())
				requestURL += $"&series={string.Join(",", Series)}";
			if (Events != null && Events.Any())
				requestURL += $"&events={string.Join(",", Events)}";
			if (Stories != null && Stories.Any())
				requestURL += $"&stories={string.Join(",", Stories)}";
			if (Order != null && Order.Any())
				requestURL += $"&orderBy={string.Join(",", Order)}";
			if (Limit.HasValue)
				requestURL += $"&limit={Limit.Value}";
			if (Offset.HasValue)
				requestURL += $"&offset={Offset.Value}";

			var url = new Uri(requestURL);

			// Make the API call
			var response = await _client.GetAsync(url);

			string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);

			CharactersDataWrapper cdw = JsonConvert.DeserializeObject<CharactersDataWrapper>(json);
			return cdw;
		}

        public async Task<CharactersDataWrapper> GetCharacter(int characterId)
        {
            string timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();

            string s = string.Format("{0}{1}{2}", timestamp, _privateKey, _publicKey);

            string hash = CreateHash(s);

            string requestURL = $"{BASE_URL}/characters/{characterId}?ts={timestamp}&apikey={_publicKey}&hash={hash}";

            var response = await _client.GetAsync( new Uri(requestURL));

            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();

            CharactersDataWrapper cdw = JsonConvert.DeserializeObject<CharactersDataWrapper>(json);

            return cdw;
        }

        public async Task<ComicDataWrapper> GetComicForCharacter(int characterId)
		{
			string timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
			string s = string.Format("{0}{1}{2}", timestamp, _privateKey, _publicKey);
			string hash = CreateHash(s);
			string requestURL = $"https://gateway.marvel.com/v1/public/characters/{characterId}/comics?ts={timestamp}&apikey={_publicKey}&hash={hash}";

			var response = await _client.GetAsync(new Uri(requestURL));

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			string json = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ComicDataWrapper>(json);
		}

        public async Task<ComicDataWrapper> GetComics(string titleStartsWith = null,
														DateTime? dateDescriptor = null,
														IEnumerable<int>? Characters = null,
														IEnumerable<int>? Series = null,
														IEnumerable<int>? Events = null,
														IEnumerable<int>? Stories = null,
														IEnumerable<OrderBy>? Order = null,
														int? Limit = 75,
														int? Offset = null)
		{
			string timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
			string s = string.Format("{0}{1}{2}", timestamp, _privateKey, _publicKey);
			string hash = CreateHash(s);

			string requestURL = $"{BASE_URL}/comics?ts={timestamp}&apikey={_publicKey}&hash={hash}";

			if (!string.IsNullOrEmpty(titleStartsWith))
				requestURL += $"&titleStartsWith={titleStartsWith}";
			if (dateDescriptor.HasValue)
				requestURL += $"&dateDescriptor={dateDescriptor.Value.ToString("yyyy-MM-dd")}";
			if (Characters != null && Characters.Any())
				requestURL += $"&characters={string.Join(",", Characters)}";
			if (Series != null && Series.Any())
				requestURL += $"&series={string.Join(",", Series)}";
			if (Events != null && Events.Any())
				requestURL += $"&events={string.Join(",", Events)}";
			if (Stories != null && Stories.Any())
				requestURL += $"&stories={string.Join(",", Stories)}";
			if (Order != null && Order.Any())
				requestURL += $"&orderBy={string.Join(",", Order)}";
			if (Limit.HasValue)
				requestURL += $"&limit={Limit.Value}";
			if (Offset.HasValue)
				requestURL += $"&offset={Offset.Value}";

			var url = new Uri(requestURL);

			var response = await _client.GetAsync(url);

			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			string json = await response.Content.ReadAsStringAsync();

			ComicDataWrapper cdw = JsonConvert.DeserializeObject<ComicDataWrapper>(json);

			return cdw;
		}

        private string CreateHash(string input)
        {
            var hash = string.Empty;
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                hash = sBuilder.ToString();
            }
            return hash;
        }

    }
}
