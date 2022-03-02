using System;
using Foundation;
using MoviesApp.Interfaces;
using MoviesApp.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppSettings))]
namespace MoviesApp.iOS.DependencyServices
{
    public class AppSettings: IAppSettingsHelper
    {
       
        public void OpenAppSettings()
        {
            var url = new NSUrl($"app-settings:");
            UIApplication.SharedApplication.OpenUrl(url);
        }
    }
}
