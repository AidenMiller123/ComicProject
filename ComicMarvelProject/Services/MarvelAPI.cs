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
        private readonly string _publicKey = "db67378ebd01d1317dd8833eec665b0a";
        private readonly string _privateKey = "f844c83d90bc6841f492906b6989920352221979";
        private static HttpClient _client = new HttpClient();

        public Marvel()
        {

        }

        public async Task<CharactersDataWrapper> GetCharcters(string Name = null,
                                                            string? NameStartsWith = null,
                                                            DateTime? ModifiedSince = null,
                                                            IEnumerable<int>? Comics = null,
                                                            IEnumerable<int>? Series = null,
                                                            IEnumerable<int>? Events = null,
                                                            IEnumerable<int>? Stories = null,
                                                            IEnumerable<OrderBy>? Order = null,
                                                            int? Limit = 30,
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

			CharactersDataWrapper cdw = JsonConvert.DeserializeObject<CharactersDataWrapper>(json);
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
