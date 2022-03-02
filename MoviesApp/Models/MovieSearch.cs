using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesApp.Models
{
    public class MovieSearch
    {
       
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("results")]
        public List<Movie> Movies { get; set; }
    }
}
