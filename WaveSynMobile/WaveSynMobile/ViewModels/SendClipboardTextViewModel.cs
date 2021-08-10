using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Essentials;

using WaveSynMobile.Utils;


namespace WaveSynMobile.ViewModels
{
    class SendClipboardTextViewModel : BaseSendViewModel
    {

        public SendClipboardTextViewModel(string ip, int port, int password, byte[] key, byte[] iv) : base(ip, port, password, key, iv)
        {
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
