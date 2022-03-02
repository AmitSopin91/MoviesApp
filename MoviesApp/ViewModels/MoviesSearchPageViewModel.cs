using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MoviesApp.Interfaces;
using MoviesApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace MoviesApp.ViewModels
{
    public class MoviesSearchPageViewModel : BindableBase, Prism.Navigation.IInitialize
    {
        protected IRequestProvider RequestProvider => DependencyService.Get<IRequestProvider>();
        public DelegateCommand SearchCommand { get; }
        public DelegateCommand<Movie> ShowMovieDetailCommand { get; }
        private readonly INavigationService navigationService;
        public DelegateCommand<Movie> MovieDetailCommand { get; }
        public DelegateCommand ItemTresholdReachedCommand { get; set; }


        private int currentPage = 1;
        private int totalPage = 0;

        private List<Genre> genres;

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                SetProperty(ref searchTerm, value);
               // SearchResults.Clear();
            }
        }

        private ObservableCollection<Movie> searchResults;
        public ObservableCollection<Movie> SearchResults
        {
            get
            {
                return searchResults;
            }
            set
            {
                SetProperty(ref searchResults, value);

            }
        }

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

        public MoviesSearchPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            SearchCommand = new DelegateCommand(async () => await LoadSearchCommand().ConfigureAwait(false));
            ShowMovieDetailCommand = new DelegateCommand<Movie>(async (Movie movie) => await LoadShowMovieDetailCommand(movie).ConfigureAwait(false));
            MovieDetailCommand = new DelegateCommand<Movie>(async (Movie movie) => await ShowMovieDetailCmd(movie).ConfigureAwait(false));
            ItemTresholdReachedCommand = new DelegateCommand(async () => await ItemsTresholdReached().ConfigureAwait(false));
            SearchResults = new ObservableCollection<Movie>();

        }

        private async Task ItemsTresholdReached()
        {
            await LoadNextPageAsync().ConfigureAwait(false);
        }

        private async Task LoadSearchCommand()
        {
            currentPage = 1;
            await LoadMoviesAsync(currentPage).ConfigureAwait(true);
        }

        private async Task LoadShowMovieDetailCommand(Movie movie)
        {
            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };
            await navigationService.NavigateAsync("MovieDetailPage", parameters).ConfigureAwait(false);
        }

        private async Task LoadMoviesAsync(int page)
        {
            genres = genres ?? await RequestProvider.GetGenresAsync().ConfigureAwait(false);
            var searchMovies = await RequestProvider.SearchMoviesAsync(searchTerm, page).ConfigureAwait(false);
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
                    SearchResults.Add(item);
                }
             //   ObservableCollection<Movie> myCollection = new ObservableCollection<Movie>(movies);
              //  SearchResults = myCollection;
            }


        }

        private async Task ShowMovieDetailCmd(Movie movie)
        {
            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };
            await navigationService.NavigateAsync("MovieDetailPage", parameters).ConfigureAwait(false);
        }

        public void Initialize(INavigationParameters parameters)
        {
            SearchTerm = parameters.GetValue<string>("scandata");
            if(!string.IsNullOrEmpty(SearchTerm))
            {
               SearchCommand.Execute();
            }    
        }

        private async Task LoadNextPageAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                await LoadMoviesAsync(currentPage).ConfigureAwait(false);
            }
        }

    }
}
