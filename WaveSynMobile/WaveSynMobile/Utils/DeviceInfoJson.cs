using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WaveSynMobile.Utils
{
    class DeviceInfoJson
    {
        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }
    }
}
