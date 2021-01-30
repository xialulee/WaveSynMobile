using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WaveSynMobile.Utils;
using Xamarin.Essentials;

namespace WaveSynMobile.ViewModels
{
    class SendFileViewModel : BaseSendViewModel
    {
        public SendFileViewModel(string ip, int port, int password, byte[] key, byte[] iv) : base(ip, port, password, key, iv)
        {
        }

        async public void Communicate()
        {
            using var communicator = new Communicator(this.ip, this.port, this.password, this.key, this.iv);

            var picked = await FilePicker.PickAsync();
            var fileName = picked.FileName;
            var inStream = await picked.OpenReadAsync();

            this.StatusHTML = "<p>Sending...</p>";
            var success = true;
            try
            {
                await Task.Run(() =>
                {
                    communicator.Connect();
                    communicator.SendStream(inStream, fileName);
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
