using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WaveSynMobile.Utils
{
    public class WaveSynCommand
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
    }

    public class AESInfo
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("iv")]
        public string IV { get; set; }
    }

    public class WaveSynBarcode
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("password")]
        public int Password { get; set; }

        [JsonPropertyName("aes")]
        public AESInfo AES { get; set; }

        [JsonPropertyName("command")]
        public WaveSynCommand Command { get; set; }
    }

    public class TextJson
    {
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
