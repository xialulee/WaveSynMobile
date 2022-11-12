using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WaveSynMobile.Utils;

namespace WaveSynMobile.ViewModels {
    internal class RecvClipboardTextViewModel : BaseViewModel {
        private string ip;
        private int port;
        private int password;
        private byte[] key;
        private byte[] iv;

        public RecvClipboardTextViewModel(string ip, int port, int password, byte[] key, byte[] iv) {
            this.ip = ip;
            this.port = port;
            this.password = password;
            this.key = key;
            this.iv = iv;
        }

        public async void Communicate() {
            using Communicator communicator = new Communicator(ip, port, password, key);

            bool success = true;

            try {
                await Task.Run(() => {
                    communicator.Connect();
                    var result = communicator.RecvText();
                });
            } catch (SocketException) {
                success = false;
            }
        }
    }
}
