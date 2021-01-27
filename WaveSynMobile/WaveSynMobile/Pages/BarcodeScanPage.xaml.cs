using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeScanPage : ContentPage
    {
        private bool Scanning = true;

        public BarcodeScanPage()
        {
            InitializeComponent();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            if (Scanning)
            {
                Scanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //Console.WriteLine(result.Text);
                    var barcode = JsonSerializer.Deserialize<Utils.WaveSynBarcode>(result.Text);
                    var resultPage = new Pages.BarcodeScanResultPage(barcode);
                    await this.Navigation.PushAsync(resultPage);
                    this.Navigation.RemovePage(this);
                });
            }
        }
    }
}