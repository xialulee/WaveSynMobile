using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

using Xamarin.Essentials;

namespace WaveSynMobile.Utils {
    class Communicator : IDisposable {
        private readonly string ip;
        private readonly int port;
        private readonly int password;
        private readonly byte[] key;
        private readonly Socket socket;
        private readonly IPEndPoint ipe;
        private Aes aes;

        public Communicator(string ip, int port, int password, byte[] key) {
            this.ip = ip;
            this.port = port;
            this.password = password;
            this.key = key;
            MakeAES();
            ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            socket = new Socket(this.ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect() {
            this.socket.Connect(this.ipe);
        }

        private void MakeAES() {
            aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.Padding = PaddingMode.PKCS7;
        }

        private byte[] MakeEncryptedInfo(string fileName="") {
            var info = new DataInfoJson() {
                Manufacturer = DeviceInfo.Manufacturer,
                Model = DeviceInfo.Model,
                FileName = fileName
            };
            var jsonStr = JsonSerializer.Serialize(info);
            var outStream = new MemoryStream();
            Encrypt(outStream, jsonStr);
            return outStream.ToArray();
        }

        private void Crypt(Stream outStream, Stream inStream, bool encrypt=true, int bufLen=65536, bool updateIV=true) {
            var buf = new byte[bufLen];
            if (encrypt && updateIV) aes.GenerateIV();
            ICryptoTransform transform;
            transform = encrypt ? aes.CreateEncryptor() : aes.CreateDecryptor();
            using var cryptStream = new CryptoStream(
                outStream,
                transform,
                CryptoStreamMode.Write);
            using var bWriter = new BinaryWriter(cryptStream);

            int readCnt;
            do {
                readCnt = inStream.Read(buf, 0, bufLen);
                bWriter.Write(buf, 0, readCnt);
            } while (readCnt > 0);
        }

        private void Encrypt(Stream outStream, string inText, int bufLen = 0) {
            if (bufLen == 0) bufLen = inText.Length; 
            var textBytes = Encoding.UTF8.GetBytes(inText);
            var inStream = new MemoryStream();
            inStream.Write(textBytes, offset: 0, count: textBytes.Length);
            inStream.Seek(0, SeekOrigin.Begin);
            Crypt(outStream, inStream, encrypt: true, bufLen: bufLen);
        }

        private string Decrypt(Stream inStream) {
            var outStream = new MemoryStream();
            Crypt(outStream, inStream, encrypt: false);
            var textBytes = outStream.ToArray();
            return Encoding.UTF8.GetString(textBytes);
        }

        public void SendHead(long encryptedDataLen, string fileName = "") {
            var encryptedInfo = MakeEncryptedInfo(fileName: fileName);
            var ivInfo = aes.IV;

            var headObj = new Utils.DataHead() {
                Password = (uint)password,
                InfoLen = (UInt64)encryptedInfo.Length,
                DataLen = (UInt64)encryptedDataLen
            };

            // Send exit flag
            socket.Send(new byte[1]);
            // Send head in cleartext. 
            SendJson(headObj);
            // Send info
            socket.Send(ivInfo);
            socket.Send(encryptedInfo);
        }

        public void SendText(string text) {
            var memStream = new MemoryStream();
            Encrypt(memStream, text);
            byte[] ivText = aes.IV;
            byte[] encryptedText = memStream.ToArray();

            SendHead(encryptedText.Length);
            socket.Send(ivText);
            socket.Send(encryptedText);
        }

        public string RecvText() {
            var encryptedInfo = MakeEncryptedInfo();
            var headObj = new DataHead() {
                Password = (uint)password,
                InfoLen = (UInt64)encryptedInfo.Length,
                DataLen = (UInt64)0
            };
            socket.Send(new byte[1] { 0 });
            SendJson(headObj);
            socket.Send(encryptedInfo);
            var buffer = new byte[65536];
            var inStream = new MemoryStream();
            int bytesRead;
            do {
                bytesRead = socket.Receive(buffer);
                inStream.Write(buffer, 0, bytesRead);
            } while (bytesRead == buffer.Length);
            return Decrypt(inStream);
        }

        public void SendStream(Stream stream, string fileName = "") {
            var encryptedDataLen = (long)Math.Ceiling(stream.Length/16.0) * 16;
            SendHead(encryptedDataLen, fileName: fileName);
            aes.GenerateIV();
            socket.Send(aes.IV);
            using var socketStream = new NetworkStream(socket);
            Crypt(outStream: socketStream, inStream: stream, updateIV: false);
        }

        public void SendJson<T>(T obj) {
            var jsonStr = JsonSerializer.Serialize<T>(obj);
            var jsonUTF8 = Encoding.UTF8.GetBytes(jsonStr);
            SendBytes(jsonUTF8);
        }

        private void SendBytes(byte[] byteArr) {
            var length = byteArr.Length;
            socket.Send(Int32ToBytes(length));
            socket.Send(byteArr);
        }

        public void Dispose() {
            socket?.Dispose();
        }

        private byte[] Int32ToBytes(Int32 integer) {
            var retval = new byte[4];
            for (int i = 0; i < 4; ++i) {
                int shift = 8 * (3 - i);
                retval[i] = (byte)((integer & (0xff << shift)) >> shift);
            }
            return retval;
        }
    }
}
