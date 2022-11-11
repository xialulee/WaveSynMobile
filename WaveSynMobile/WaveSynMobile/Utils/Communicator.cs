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
        private readonly byte[] iv;
        private readonly Socket socket;
        private readonly IPEndPoint ipe;
        private Aes aes;
        private ICryptoTransform encryptor = null;
        private ICryptoTransform decryptor = null;

        public Communicator(string ip, int port, int password, byte[] key, byte[] iv) {
            this.ip = ip;
            this.port = port;
            this.password = password;
            this.key = key;
            this.iv = iv;
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
            aes.IV = iv;
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

        private void Crypt(Stream outStream, Stream inStream, bool encrypt=true, int bufLen=65536) {
            var buf = new byte[bufLen];
            ICryptoTransform transform;

            if (encrypt) {
                if (encryptor == null) {
                    encryptor = aes.CreateEncryptor();
                }
                transform = encryptor;
            } else {
                if (decryptor == null) {
                    decryptor = aes.CreateDecryptor();
                }
                transform = decryptor;
            }

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

        private void Encrypt(Stream outStream, string text, int bufLen = 0) {
            if (bufLen == 0) { bufLen = text.Length; }
            var textBytes = Encoding.UTF8.GetBytes(text);
            var inStream = new MemoryStream();
            inStream.Write(textBytes, 0, textBytes.Length);
            inStream.Seek(0, SeekOrigin.Begin);
            Crypt(outStream, inStream, true, bufLen);
        }

        private string Decrypt(Stream inStream) {
            inStream.Seek(0, SeekOrigin.Begin);
            var outStream = new MemoryStream();
            Crypt(outStream, inStream, false);
            var textBytes = outStream.ToArray();
            return Encoding.UTF8.GetString(textBytes);
        }

        public void SendHead() {
            var passwordArr = Int32ToBytes(password);

            // Send exit flag
            socket.Send(new byte[1]);

            // Send password
            socket.Send(passwordArr);

            // Send device info
            SendJson(new DataInfoJson() {
                Manufacturer = DeviceInfo.Manufacturer,
                Model = DeviceInfo.Model
            });
        }

        public void SendText(string text) {
            var encryptedInfo = MakeEncryptedInfo();
            var memStream = new MemoryStream();
            Encrypt(memStream, text);
            var encryptedText = memStream.ToArray();
            var headObj = new Utils.DataHead() {
                Password = (uint)this.password,
                InfoLen = (UInt64)encryptedInfo.Length,
                DataLen = (UInt64)encryptedText.Length
            };

            // Send exit flag
            this.socket.Send(new byte[1]);
            // Send head
            SendJson(headObj);
            // Send info
            this.socket.Send(encryptedInfo);
            // Send data
            this.socket.Send(encryptedText);
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
            var encryptedInfo = MakeEncryptedInfo(fileName);
            var encryptedDataLen = (long)Math.Ceiling(stream.Length/16.0) * 16;
            var headObj = new DataHead() {
                Password = (uint)this.password,
                InfoLen = (UInt64)encryptedInfo.Length,
                DataLen = (UInt64)encryptedDataLen
            };

            // Send exit flag
            this.socket.Send(new byte[1]);
            // Send head
            SendJson(headObj);
            // Send info
            this.socket.Send(encryptedInfo);
            // Send stream
            using var socketStream = new NetworkStream(this.socket);
            Crypt(outStream: socketStream, inStream: stream);
        }

        public void SendJson<T>(T obj) {
            var jsonStr = JsonSerializer.Serialize<T>(obj);
            var jsonUTF8 = Encoding.UTF8.GetBytes(jsonStr);
            this.SendBytes(jsonUTF8);
        }

        private void SendBytes(byte[] byteArr) {
            var length = byteArr.Length;
            socket.Send(Int32ToBytes(length));
            socket.Send(byteArr);
        }

        public void Dispose() {
            this.socket?.Dispose();
        }

        private byte[] Int32ToBytes(Int32 integer) {
            var retval = new byte[4];
            for (int i = 0; i < 4; ++i) {
                var shift = 8 * (3 - i);
                retval[i] = (byte)((integer & (0xff << shift)) >> shift);
            }
            return retval;
        }
    }
}
