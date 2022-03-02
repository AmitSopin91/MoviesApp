using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesApp.Models
{
    public class MovieImage
    {
        [JsonProperty("id")]
        public int Id { get; set; }

       
        [JsonProperty("backdrops")]
        public List<MovieImg> Backdrops { get; set; }

        [JsonProperty("posters")]
        public List<posters> Posters { get; set; }
    }

    public class posters
    {
        [JsonProperty("aspect_ratio")]
        public float AspectRatio { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }

       [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
