using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using WaveSynMobile.Utils;
using System.Threading.Tasks;

namespace WaveSynMobile.ViewModels
{
    class BarcodeScanResultViewModel : BaseViewModel
    {
        private string ip;
        private int port;
        private int password;

        private string statusHTML;
        public string StatusHTML
        {
            get => this.statusHTML;
            set => this.SetProperty(ref this.statusHTML, value);
        }

        public BarcodeScanResultViewModel(string ip, int port, int password)
        {
            this.ip = ip;
            this.port = port;
            this.password = password;
        }

        async public void Communicate()
        {
            using (var communicator = new Communicator(this.ip, this.port, this.password))
            {
                var clipbText = await Clipboard.GetTextAsync();

                this.StatusHTML = "<p>Sending...</p>";
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
                    this.StatusHTML = $"<p style=\"color:red\">{ex.Message}</p>";
                }

                if (success)
                {
                    this.StatusHTML = "<p style=\"color:green\">Finished.</p>";
                }
            }
        }
    }
}
