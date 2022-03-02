using System;
using Android.App;
using Android.Content;
using MoviesApp.Droid.DependencyServices;
using MoviesApp.Interfaces;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(AppSettings))]
namespace MoviesApp.Droid.DependencyServices
{
    public class AppSettings: IAppSettingsHelper
    {
        
        public void OpenAppSettings()
        {
            var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            string package_name = Application.Context.PackageName;
            var uri = Android.Net.Uri.FromParts("package", package_name, null);
            intent.SetData(uri);
            Application.Context.StartActivity(intent);
        }
    }
}
