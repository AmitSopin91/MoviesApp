using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace MoviesApp.ViewModels
{
    public class ScanPageViewModel : BindableBase
    {

        private readonly INavigationService navigationService;

        private ZXing.Result result;
        public ZXing.Result Result
        {
            get { return this.result; }
            set
            {
                if (!string.Equals(this.result, value))
                {
                    this.result = value;
                    RaisePropertyChanged(nameof(Result));
                }
            }
        }

        public bool isVisible = true;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {

                isVisible = value;
                RaisePropertyChanged(nameof(IsVisible));

            }
        }


        public bool isPermissionVisible = false;
        public bool IsPermissionVisible
        {
            get { return isPermissionVisible; }
            set
            {

                isPermissionVisible = value;
                RaisePropertyChanged(nameof(IsPermissionVisible));

            }
        }


        public bool _isScanning = true;
        public bool IsScanning
        {
            get { return _isScanning; }
            set
            {

                _isScanning = value;
                RaisePropertyChanged(nameof(IsScanning));

            }
        }


        public bool _isAnalyzing = true;
        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set
            {

                _isAnalyzing = value;
                RaisePropertyChanged(nameof(IsAnalyzing));

            }
        }

        public DelegateCommand<object> QRScanResultCommand { get; }

        public ScanPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            QRScanResultCommand = new DelegateCommand<object>(async (object obj) => await ExecuteShowMovieDetailCommand(obj).ConfigureAwait(false));

        }


        private async Task ExecuteShowMovieDetailCommand(object obj)
        {
            ZXing.Result scandata = (ZXing.Result)obj;

            var parameters = new NavigationParameters
            {
                { "scandata", scandata.Text }
            };
            Device.BeginInvokeOnMainThread(async () => await navigationService.NavigateAsync("MoviesSearchPage", parameters).ConfigureAwait(false));

        }

    }
}

