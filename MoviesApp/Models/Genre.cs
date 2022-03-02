using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesApp.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GenreList
    {
       
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }
    }
}
