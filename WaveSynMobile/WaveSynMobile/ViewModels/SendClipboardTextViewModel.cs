using System.Threading.Tasks;
using Xamarin.Essentials;
using WaveSynMobile.Utils;


namespace WaveSynMobile.ViewModels {
    internal class SendClipboardTextViewModel : BaseSendViewModel {

        public SendClipboardTextViewModel(string ip, int port, int password, byte[] key, byte[] iv) : base(ip, port, password, key, iv) {
        }

        public async void Communicate() {
            using Communicator communicator = new Communicator(ip, port, password, key, iv);

            if (!Clipboard.HasText) {
                StatusHTML = "<p style=\"color:red\">Clipboard does not have text.</p>";
                return;
            }

            string clipbText = await Clipboard.GetTextAsync();
            StatusHTML = "<p>Sending...</p>";
            bool success = true;

            try {
                await Task.Run(() => {
                    communicator.Connect();
                    communicator.SendText(clipbText);
                });
            } catch (System.Net.Sockets.SocketException ex) {
                success = false;
                StatusHTML = $"<p style=\"color:red\">{ex.Message}</p>";
            }

            if (success) {
                StatusHTML = "<p style=\"color:green\">Finished.</p>";
            }
        }
    }
}
