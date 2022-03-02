using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Interfaces;
using MoviesApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using static Xamarin.Essentials.Permissions;

namespace MoviesApp.Views
{
    public partial class ScanPage : ContentPage
    {
      //  ZXingScannerView scanView;
       
        public ScanPage()
        {
            InitializeComponent();
            //BindingContext = scanViewModel;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<IAppSettingsHelper>().OpenAppSettings();
        }

        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //try
            //{
            //    var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            //    if (status != PermissionStatus.Granted)
            //    {
            //        if (Device.RuntimePlatform == Device.iOS)
            //        {
            //            var statusios = await Permissions.RequestAsync<Permissions.Camera>();
            //            if (statusios != PermissionStatus.Granted)
            //            {
            //                scanViewModel.IsPermissionVisible = true;
            //            }
            //            else
            //            {
            //              //  CreateScanner();

            //            }
            //        }
            //        else
            //        {

            //          //  scanViewModel.IsPermissionVisible = true;
            //        }

            //    }
            //    else
            //    {
            //      //  CreateScanner();
            //    }
            //}
            //catch (System.UnauthorizedAccessException ex)
            //{
                
            //}
        }

        //void CreateScanner()
        //{
        //    scanViewModel.IsPermissionVisible = false;
        //    if (scanView == null)
        //    {
        //        scanView = new ZXingScannerView()
        //        {
        //            HorizontalOptions = LayoutOptions.FillAndExpand,
        //            VerticalOptions = LayoutOptions.FillAndExpand,

        //        };

        //        scanView.SetBinding(ZXingScannerView.IsVisibleProperty, nameof(scanViewModel.IsVisible));
        //        scanView.SetBinding(ZXingScannerView.IsAnalyzingProperty, nameof(scanViewModel.IsAnalyzing));
        //        scanView.SetBinding(ZXingScannerView.IsScanningProperty, nameof(scanViewModel.IsScanning));
        //        scanView.SetBinding(ZXingScannerView.ResultProperty, nameof(scanViewModel.Result), BindingMode.TwoWay);
        //        scanView.SetBinding(ZXingScannerView.ScanResultCommandProperty, nameof(scanViewModel.QRScanResultCommand));
        //        scanView.Options = new MobileBarcodeScanningOptions()
        //        {

        //            TryHarder = true,
        //            AssumeGS1 = true,
        //            DelayBetweenAnalyzingFrames = 3000,
        //            DelayBetweenContinuousScans = 3000,

        //        };

        //        List<BarcodeFormat> barcodes = new List<BarcodeFormat>() { BarcodeFormat.DATA_MATRIX, BarcodeFormat.QR_CODE, BarcodeFormat.EAN_13, BarcodeFormat.CODE_128 };
        //        scanView.Options.PossibleFormats = barcodes;

        //        scannerview.Children.Add(scanView);
        //    }
        //    scanViewModel.IsVisible = true;
        //    scanViewModel.IsScanning = true;
        //    scanViewModel.IsAnalyzing = true;

        //}

        protected override void OnDisappearing()
        {
           // scanViewModel.IsAnalyzing = false;
            base.OnDisappearing();
        }

        void scanView_OnScanResult(ZXing.Result result)
        {

        }
    }
}
