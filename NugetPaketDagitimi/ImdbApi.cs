using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ImdbFilmPoster
{
    public class ImdbApi
    {
        private const string ApiBase = "http://mymovieapi.com/?format=json";
        private const string FilmQuery = ApiBase + "&id={0}";

        public async static Task<FilmModel> RetrieveFilmModel(string filmImdbId)
        {
            if(String.IsNullOrEmpty(filmImdbId))
                throw new ArgumentException("filmImdbId");

            var response = await HttpHelper.GetIgnoringErrors(String.Format(FilmQuery, filmImdbId));
            return JsonConvert.DeserializeObject<FilmModel>(response);
        }

        
        public class FilmModel
        {
            [JsonProperty(PropertyName = "poster")]
            public FilmPosters ImdbPoster { get; set; }
        }

        public class FilmPosters
        {
            [JsonProperty(PropertyName = "imdb")]
            public string ImdbPoster { get; set; }

            [JsonProperty(PropertyName = "cover")]
            public string Cover { get; set; }
        }

    }
}
