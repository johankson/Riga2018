using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeetPhotos.Core.Net
{
	public class RestClient : IRestClient
    {
        private static HttpClient _httpClient = new HttpClient();

        public async Task<T> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                json = json.TrimStart("jsonFlickrFeed(".ToCharArray()).TrimEnd(')');

				return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }

            throw new Exception("Hä gick in nööö");
        }
    }
}
