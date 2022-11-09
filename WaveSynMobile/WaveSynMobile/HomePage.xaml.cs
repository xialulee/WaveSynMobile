using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace WaveSynMobile {
    public partial class HomePage : ContentPage {
        public HomePage() {
            InitializeComponent();
        }

        private async void OnScanClicked(object sender, EventArgs e) {
            var scanPage = new Views.BarcodeScanPage();
            await Navigation.PushAsync(scanPage);
        }

        async private void OnEquationsClicked(object sender, EventArgs e) {
            var equationsPage = new Views.EquationSelectPage();
            await Navigation.PushAsync(equationsPage);
        }

        private async void Button_Clicked(object sender, EventArgs e) {
            await PickAndShow();
        }

        private async Task PickAndShow(PickOptions options = null) {
            try {
                FileResult result = await FilePicker.PickAsync();
                if (result != null) {
                    Console.WriteLine($"File Name: {result.FileName}");
                    System.IO.Stream stream = await result.OpenReadAsync();
                    var buf1 = new byte[16];
                    int readCnt = stream.Read(buf1, 0, 16);
                    var buf2 = new byte[16];
                    int readCnt2 = stream.Read(buf2, 0, 16);
                    Console.WriteLine(readCnt2);
                    /*if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        // var stream = await result.OpenReadAsync();
                        // Image = ImageSource.FromStream(() => stream);
                    }*/
                }
            } catch (Exception) {
                // The user canceled or something went wrong
            }
        }
    }
}
