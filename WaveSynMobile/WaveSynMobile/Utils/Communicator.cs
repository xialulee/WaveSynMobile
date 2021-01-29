using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

using Xamarin.Essentials;

namespace WaveSynMobile.Utils
{
    class Communicator : IDisposable
    {
        private readonly string ip;
        private readonly int port;
        private readonly int password;
        private readonly byte[] key;
        private readonly byte[] iv;
        private readonly Socket socket;
        private readonly IPEndPoint ipe;


        public Communicator(string ip, int port, int password, byte[] key, byte[] iv) 
        {
            this.ip = ip;
            this.port = port;
            this.password = password;
            this.key = key;
            this.iv = iv;
            this.ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            this.socket = new Socket(this.ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }


        public void Connect()
        {
            this.socket.Connect(this.ipe);
        }


        private byte[] MakeEncryptedInfo()
        {
            var info = new DeviceInfoJson()
            {
                Manufacturer = DeviceInfo.Manufacturer,
                Model = DeviceInfo.Model
            };
            var jsonStr = JsonSerializer.Serialize(info);
            return encrypt(jsonStr);
        }


        private byte[] encrypt(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);

            using Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Key = this.key;
            aes.IV = this.iv;
            aes.Padding = PaddingMode.PKCS7;

            using var outStream = new MemoryStream();

            {
                using var cryptStream = new CryptoStream(
                                            outStream,
                                            aes.CreateEncryptor(),
                                            CryptoStreamMode.Write);
                using var bWriter = new BinaryWriter(cryptStream);
                bWriter.Write(textBytes);
            }
                
            return outStream.ToArray();
        }


        public void SendHead()
        {
            var passwordArr = this.Int32ToBytes(this.password);

            // Send exit flag
            this.socket.Send(new byte[1]);

            // Send password
            this.socket.Send(passwordArr);

            // Send device info
            this.SendJson(new DeviceInfoJson()
            {
                Manufacturer = DeviceInfo.Manufacturer,
                Model = DeviceInfo.Model
            });
        }


        public void SendText(string text)
        {
            /*
            var textJson = new TextJson();
            textJson.Data = text;
            var jsonBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize<TextJson>(textJson));
            this.SendBytes(jsonBytes);
            */
            var encryptedInfo = MakeEncryptedInfo();
            var encryptedText = encrypt(text);
            var headObj = new Utils.DataHead()
            {
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


        public void SendJson<T>(T obj)
        {
            var jsonStr = JsonSerializer.Serialize<T>(obj);
            var jsonUTF8 = Encoding.UTF8.GetBytes(jsonStr);
            this.SendBytes(jsonUTF8);
        }


        private void SendBytes(byte[] byteArr)
        {
            var length = byteArr.Length;
            this.socket.Send(this.Int32ToBytes(length));
            this.socket.Send(byteArr);
        }


        public void Dispose()
        {
            this.socket?.Dispose();
        }


        private byte[] Int32ToBytes(Int32 integer)
        {
            var retval = new byte[4];
            for (int i = 0; i < 4; ++i)
            {
                var shift = 8 * (3 - i);
                retval[i] = (byte)((integer & (0xff << shift)) >> shift);
            }
            return retval;
        }
    }
}
