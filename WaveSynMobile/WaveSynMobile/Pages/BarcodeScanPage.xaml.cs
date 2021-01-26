using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeScanPage : ContentPage
    {
        public BarcodeScanPage()
        {
            InitializeComponent();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Console.WriteLine(result.Text);
            });
        }
    }
}