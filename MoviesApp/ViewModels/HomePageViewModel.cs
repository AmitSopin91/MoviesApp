using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MoviesApp.Interfaces;
using MoviesApp.Models;
using MoviesApp.Utility.Enum;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace MoviesApp.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        protected IRequestProvider RequestProvider => DependencyService.Get<IRequestProvider>();
        private int currentPage = 1;
        private int totalPage = 0;

        ObservableCollection<Movie> movieList;
        public ObservableCollection<Movie> MovieList
        {
            get { return movieList; }
            set
            {
                SetProperty(ref movieList, value);
            }
        }

        private int itemTreshold;
        public int ItemTreshold
        {
            get { return itemTreshold; }
            set
            {
                itemTreshold = value;
                RaisePropertyChanged("ItemTreshold");
            }
        }

        public DelegateCommand LoadUpcomingMoviesCommand { get; }
        public DelegateCommand LoadMoviesSearchCommand { get; }
        public DelegateCommand<Movie> MovieDetailCommand { get; }
        public DelegateCommand LoadScanSearchCommand { get; }
        public DelegateCommand ItemTresholdReachedCommand { get; set; }
        private List<Genre> genres;
        public ObservableCollection<Movie> Movies { get; set; }

        private readonly INavigationService navigationService;
        public HomePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            LoadUpcomingMoviesCommand = new DelegateCommand(async () => await ShowUpcomingMoviesCommand().ConfigureAwait(false));
            MovieDetailCommand = new DelegateCommand<Movie>(async (Movie movie) => await ShowMovieDetailCmd(movie).ConfigureAwait(false));
            LoadMoviesSearchCommand = new DelegateCommand(async () => await ShowSearchMoviesCommand().ConfigureAwait(false));
            LoadScanSearchCommand = new DelegateCommand(async () => await ShowSacnSearchMoviesCommand().ConfigureAwait(false));
            ItemTresholdReachedCommand = new DelegateCommand(async () => await ItemsTresholdReached().ConfigureAwait(false));
            MovieList = new ObservableCollection<Movie>();
            LoadUpcomingMoviesCommand.Execute();
        }

        private async Task ItemsTresholdReached()
        {
            await LoadNextPageUpcomingMoviesAsync().ConfigureAwait(false);
        }

        private async Task ShowSacnSearchMoviesCommand()
        {
            await navigationService.NavigateAsync("ScanPage").ConfigureAwait(false);
        }

        private async Task ShowMovieDetailCmd(Movie movie)
        {
            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };
            await navigationService.NavigateAsync("MovieDetailPage", parameters).ConfigureAwait(false);
        }

        private async Task ShowUpcomingMoviesCommand()
        {
            currentPage = 1;
            ItemTreshold = 1;
            await LoadMoviesAsync(currentPage, Enums.MovieCategory.Upcoming).ConfigureAwait(false);
        }


        private async Task LoadMoviesAsync(int page, Enums.MovieCategory movieCategory)
        {
            genres = genres ?? await RequestProvider.GetGenresAsync().ConfigureAwait(false);
            var searchMovies = await RequestProvider.GetMoviesByCategoryAsync(page, movieCategory).ConfigureAwait(false);
            if (searchMovies != null)
            {
                var movies = new List<Movie>();
                totalPage = searchMovies.TotalPages;
                foreach (var movie in searchMovies.Movies)
                {
                    if (movie.GenreIds != null)
                    {
                        movie.Genres =
                            movie.Genres ??
                            genres.Where(genre => movie.GenreIds.Any(genreId => genreId == genre.Id)).ToArray();
                    }

                    movies.Add(movie);
                }
                foreach (var item in movies)
                {
                    MovieList.Add(item);
                }

//ObservableCollection<Movie> myCollection = new ObservableCollection<Movie>(movies);
     //           MovieList = myCollection;
            }

        }

        private async Task ShowSearchMoviesCommand()
        {
            await navigationService.NavigateAsync("MoviesSearchPage").ConfigureAwait(false);
        }

        public async Task LoadNextPageUpcomingMoviesAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                await LoadMoviesAsync(currentPage, Enums.MovieCategory.Upcoming).ConfigureAwait(false);
            }
        }

    }
}


