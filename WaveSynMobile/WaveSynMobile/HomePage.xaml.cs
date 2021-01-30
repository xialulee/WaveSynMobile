using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Text.Json;

namespace WaveSynMobile
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        async private void OnScanClicked(object sender, EventArgs e)
        {
            var scanPage = new Views.BarcodeScanPage();
            await this.Navigation.PushAsync(scanPage);
        }

        
        //async private void Button_Clicked(object sender, EventArgs e)
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PickAndShow();
        }

        async Task PickAndShow(PickOptions options=null)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    Console.WriteLine($"File Name: {result.FileName}");
                    var stream = await result.OpenReadAsync();
                    var buf1 = new byte[16];
                    var readCnt = stream.Read(buf1, 0, 16);
                    var buf2 = new byte[16];
                    var readCnt2 = stream.Read(buf2, 0, 16);
                    Console.WriteLine(readCnt2);
                    /*if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        // var stream = await result.OpenReadAsync();
                        // Image = ImageSource.FromStream(() => stream);
                    }*/
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }
    }
}
