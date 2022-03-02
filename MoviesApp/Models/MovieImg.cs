using System;
using Newtonsoft.Json;

namespace MoviesApp.Models
{
    public class MovieImg
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
