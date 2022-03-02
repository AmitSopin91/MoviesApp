using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MoviesApp.Interfaces;
using MoviesApp.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace MoviesApp.ViewModels
{
    public class MovieDetailPageViewModel: BindableBase, Prism.Navigation.IInitialize
    {
        protected IRequestProvider RequestProvider => DependencyService.Get<IRequestProvider>();

        private Movie movie;
        public Movie Movie
        {
            get { return movie; }
            set { SetProperty(ref movie, value); }
        }

        private ObservableCollection<MovieImg> movieImg;
        public ObservableCollection<MovieImg> MovieImg
        {
            get { return movieImg; }
            set { SetProperty(ref movieImg, value); }
        }
  

        public MovieDetailPageViewModel()
        {
            movieImg = new ObservableCollection<MovieImg>();
        }

        private async Task LoadMovieDetailAsync(int movieId)
        {
            var movieDetail = await RequestProvider.GetMovieDetailAsync(movieId).ConfigureAwait(false);
            if (movieDetail != null)
            {
                Movie = movieDetail;
            }
        }

        private async Task LoadMovieImagesAsync(int movieId)
        {
            var movieImages = await RequestProvider.GetMovieImagesAsync(movieId).ConfigureAwait(false);
            if (movieImages != null)
            {
              var MI = movieImages.Backdrops.Where(x=> x.FilePath != movie.BackdropPath);
               ObservableCollection<MovieImg> myCollection = new ObservableCollection<MovieImg>(MI);
                MovieImg = myCollection;
            }
        }     

        public async void Initialize(INavigationParameters parameters)
        {
            Movie = parameters.GetValue<Movie>("movie");
            movieImg.Clear();
            movieImg.Add(new MovieImg
            {
                FilePath = movie.BackdropPath
            });

            await LoadMovieDetailAsync(Movie.Id).ConfigureAwait(false);
            await LoadMovieImagesAsync(Movie.Id).ConfigureAwait(false);
        }
    }
}
