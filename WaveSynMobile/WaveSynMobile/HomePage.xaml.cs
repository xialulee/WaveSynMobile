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
            var scanPage = new Pages.BarcodeScanPage();
            await this.Navigation.PushAsync(scanPage);
        }

        
        //async private void Button_Clicked(object sender, EventArgs e)
        private void Button_Clicked(object sender, EventArgs e)
        {
            /*
            var barcode = new Utils.WaveSynBarcode();
            barcode.Ip = "192.168.100.2";
            barcode.Port = 10000;
            //barcode.Password = 42674;

            var command = new Utils.WaveSynCommand();
            command.Action = "read";
            command.Source = "clipboard";
            barcode.Command = command;
            var str = JsonSerializer.Serialize<Utils.WaveSynBarcode>(barcode);
            Console.WriteLine(str);
            */

            /*
            var clipbText = await Clipboard.GetTextAsync();
            Console.WriteLine(clipbText);
            */
            const int devInfoLen = 32;
            var devInfo = $"{DeviceInfo.Manufacturer} {DeviceInfo.Model}";
            Console.WriteLine(devInfo);
            /*var devInfoArr = new byte[devInfoLen];
            for (int i=0; i<Math.Min(devInfoLen, devInfo.Count()); ++i)
            {
                devInfoArr[i] = (byte)devInfo[i];
            }
            Console.WriteLine(devInfoArr);*/
        }
    }
}
