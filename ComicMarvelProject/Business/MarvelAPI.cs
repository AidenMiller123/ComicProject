using ComicMarvelProject.Models;
using MailKit.Search;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace ComicMarvelProject.MarvelAPI
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
                                                            int? Limit = null,
                                                            int? Offset = null)
        {
            //We need a timestamp
            string timestamp = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            //we need use a has to call the marvel api
            string s = String.Format("{0}{1}{2}", timestamp, _privateKey, _publicKey);

            string hash = CreateHash(s);

            //format the url string with search critieria
            string requesetURL = String.Format(BASE_URL, "/characters?ts={0}&apikey={1}&hash={2}&name={3}", timestamp, _publicKey, hash, Name );

            var url = new Uri(requesetURL);

            var response = await _client.GetAsync(url);

            string json;

            using (var content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }

            CharactersDataWrapper cdw = JsonConvert.DeserializeObject<CharactersDataWrapper>(json);
            return cdw;
        }
        private string CreateHash(string input)
        {
            var hash = String.Empty;
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
