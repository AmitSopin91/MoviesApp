using System;
using System.Linq;
using Newtonsoft.Json;

namespace MoviesApp.Models
{
    public class Movie
    {
      
        [JsonProperty("id")]
        public int Id { get; set; }

        
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

      
        [JsonProperty("video")]
        public bool Video { get; set; }

       
        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }


        [JsonProperty("title")]
        public string Title { get; set; }

       
        [JsonProperty("popularity")]
        public float Popularity { get; set; }

      
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

       
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

       
        [JsonProperty("genres")]
        public Genre[] Genres { get; set; }

        
        [JsonProperty("genre_ids")]
        public int?[] GenreIds { get; set; }

        
        public string GenresNames
        {
            get
            {
                return (Genres != null && Genres.Count() > 0) ?
                    Genres.Select(x => x.Name).Aggregate((first, second) => $"{first}, {second}") :
                    "Undefined";
            }
        }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        
        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonIgnore]
        public bool HasOverview { get => !string.IsNullOrWhiteSpace(Overview); }

       
        [JsonProperty("release_date")]
        public DateTimeOffset? ReleaseDate { get; set; }
    }

    public class MovieDetail : Movie
    {
        //[JsonProperty("belongs_to_collection")]
        //public BelongsToCollection BelongsToCollection { get; set; }

        [JsonProperty("budget")]
        public int? Budget { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }

       [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        //[JsonProperty("production_companies")]
        //public ProductionCompanies[] ProductionCompanies { get; set; }

        //[JsonProperty("production_countries")]
        //public ProductionCountries[] ProductionCountries { get; set; }

        [JsonProperty("revenue")]
        public int? Revenue { get; set; }

        [JsonProperty("runtime")]
        public int? Runtime { get; set; }

        //[JsonProperty("spoken_languages")]
        //public SpokenLanguages[] SpokenLanguages { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }
    }
}
