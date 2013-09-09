using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImdbFilmPoster
{
    public class HttpHelper
    {
        public async static Task<String> GetIgnoringErrors(string url)
        {
            String result = String.Empty;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

    }
}
