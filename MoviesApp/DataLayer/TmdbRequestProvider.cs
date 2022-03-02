using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MoviesApp.Interfaces;
using MoviesApp.Models;
using MoviesApp.Utility;
using MoviesApp.Utility.Enum;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(MoviesApp.DataLayer.TmdbRequestProvider))]
namespace MoviesApp.DataLayer
{
    public class TmdbRequestProvider: IRequestProvider
    {

        private readonly string language;

        private HttpClient httpClient;

        public TmdbRequestProvider()
        {
            language = CultureInfo.CurrentCulture.Name;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        ~TmdbRequestProvider()
        {
            httpClient.Dispose();
        }

        

        
        public async Task<List<Genre>> GetGenresAsync()
        {
            var restUrl = $"{GlobalDefs.baseUrl}{GlobalDefs.genreListPath}?api_key={GlobalDefs.apiKey}&language={language}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            var genreList = JsonConvert.DeserializeObject<GenreList>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                            return genreList?.Genres;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               // ReportError(ex);
            }

            return null;
        }

        public async Task<MovieSearch> GetMoviesByCategoryAsync(int page, Enums.MovieCategory category)
        {
            var restUrl = $"{GlobalDefs.baseUrl}{Enums.PathCategoryMovie(category)}?api_key={GlobalDefs.apiKey}&page={page}&language={language}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<MovieSearch>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }
        public async Task<MovieDetail> GetMovieDetailAsync(int id)
        {
            var restUrl = $"{GlobalDefs.baseUrl}{GlobalDefs.moviePath}/{id}?api_key={GlobalDefs.apiKey}&language={language}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<MovieDetail>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

            return null;
        }

        public async Task<MovieImage> GetMovieImagesAsync(int id)
        {
            var restUrl = $"{GlobalDefs.baseUrl}{GlobalDefs.moviePath}/{id}/images?api_key={GlobalDefs.apiKey}";
            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<MovieImage>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }



     
       
        public async Task<MovieSearch> SearchMoviesAsync(string searchTerm, int page)
        {
            var restUrl = $"{GlobalDefs.baseUrl}{GlobalDefs.searchMoviePath}?api_key={GlobalDefs.apiKey}&query={searchTerm}&page={page}&language={language}";

            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<MovieSearch>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

            return null;
        }
    }
}
