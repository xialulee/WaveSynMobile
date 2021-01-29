using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WaveSynMobile.Utils
{
    class DataHead
    {
        [JsonPropertyName("password")]
        public uint Password { get; set; }

        [JsonPropertyName("info_len")]
        public UInt64 InfoLen { get; set; }

        [JsonPropertyName("data_len")]
        public UInt64 DataLen { get; set; }
    }
}
