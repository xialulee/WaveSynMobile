using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;

using Xamarin.Essentials;

namespace WaveSynMobile.Utils
{
    class Communicator : IDisposable
    {
        private string ip;
        private int port;
        private int password;
        private readonly Socket socket;
        private readonly IPEndPoint ipe;


        public Communicator(string ip, int port, int password) 
        {
            this.ip = ip;
            this.port = port;
            this.password = password;
            this.ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            this.socket = new Socket(this.ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }


        public void Connect()
        {
            this.socket.Connect(this.ipe);
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
            var textJson = new TextJson();
            textJson.Data = text;
            var jsonBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize<TextJson>(textJson));
            this.SendBytes(jsonBytes);
        }


        public void SendJson<T>(T obj)
        {
            var jsonStr = JsonSerializer.Serialize<T>(obj);
            var jsonUTF8 = Encoding.UTF8.GetBytes(jsonStr);
            this.SendBytes(jsonUTF8);
        }


        public void SendBytes(byte[] byteArr)
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
