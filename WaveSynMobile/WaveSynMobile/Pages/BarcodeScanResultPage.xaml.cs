using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WaveSynMobile.Utils;

namespace WaveSynMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeScanResultPage : ContentPage
    {
        private readonly WaveSynBarcode barcode = null;
        
        public BarcodeScanResultPage()
        {
            InitializeComponent();
        }

        public BarcodeScanResultPage(WaveSynBarcode barcode)
        {
            InitializeComponent();
            this.barcode = barcode;
            Console.WriteLine("----------------------------------");
            Console.WriteLine(barcode.Ip);
            Console.WriteLine(barcode.Port);
            this.Communicate();
        }

        async private void Communicate() 
        {
            using (var communicator = new Communicator(this.barcode.Ip, this.barcode.Port, this.barcode.Password))
            {
                var clipbText = await Clipboard.GetTextAsync();
                var statusLabel = this.FindByName<Label>("StatusLabel");
                statusLabel.Text = "Sending...";
                var success = true;
                try
                {
                    await Task.Run(() =>
                    {
                        communicator.Connect();
                        communicator.SendHead();
                        communicator.SendText(clipbText);
                    });
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    success = false;
                    statusLabel.Text = $"<p style=\"color:red\">{ex.Message}</p>";
                }

                if (success) 
                {
                    statusLabel.Text = "<p style=\"color:green\">Finished.</p>";
                }
            }
        }
    }
}