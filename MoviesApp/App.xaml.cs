using System;
using MoviesApp.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoviesApp
{
    public partial class App : PrismApplication
    {
        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(HomePage)}");
            this.Resources = new Styles.DefaultTheme().Resources;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<MovieDetailPage>();
            containerRegistry.RegisterForNavigation<MoviesSearchPage>();
            containerRegistry.RegisterForNavigation<ScanPage>();

        }


    }
}
