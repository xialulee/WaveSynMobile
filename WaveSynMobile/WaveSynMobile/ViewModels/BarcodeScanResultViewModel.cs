using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Essentials;

using WaveSynMobile.Utils;


namespace WaveSynMobile.ViewModels
{
    class BarcodeScanResultViewModel : BaseViewModel
    {
        private string ip;
        private int port;
        private int password;
        private byte[] key;
        private byte[] iv;

        private string statusHTML;
        public string StatusHTML
        {
            get => this.statusHTML;
            set => this.SetProperty(ref this.statusHTML, value);
        }

        public BarcodeScanResultViewModel(string ip, int port, int password, byte[] key, byte[] iv)
        {
            this.ip = ip;
            this.port = port;
            this.password = password;
            this.key = key;
            this.iv = iv;
        }



        async public void Communicate()
        {
            using (var communicator = new Communicator(this.ip, this.port, this.password, this.key, this.iv))
            {
                var clipbText = await Clipboard.GetTextAsync();

                this.StatusHTML = "<p>Sending...</p>";
                var success = true;
                try
                {
                    await Task.Run(() =>
                    {
                        communicator.Connect();
                        communicator.SendText(clipbText);
                        //var encText = encrypt(clipbText);
                        // communicator.SendHead();
                        //communicator.SendText(clipbText);
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
