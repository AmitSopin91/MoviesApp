using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Models;
using MoviesApp.Utility.Enum;

namespace MoviesApp.Interfaces
{
    public interface IRequestProvider
    {
        Task<MovieSearch> SearchMoviesAsync(string searchTerm, int page);

        
        Task<MovieSearch> GetMoviesByCategoryAsync(int page, Enums.MovieCategory sortBy);

        
        Task<MovieDetail> GetMovieDetailAsync(int id);

        
        Task<MovieImage> GetMovieImagesAsync(int id);
      
       // Task<MovieVideo> GetMovieVideosAsync(int id);

       
        Task<List<Genre>> GetGenresAsync();
    }
}
