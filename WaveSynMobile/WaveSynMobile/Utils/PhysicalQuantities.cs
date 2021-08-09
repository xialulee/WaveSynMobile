using System;
using System.Collections.Generic;
using System.Text;

namespace WaveSynMobile.Utils
{
    class PhysicalQuantities
    {
        public static Dictionary<string, double> Units = new Dictionary<string, double>
        {
            { "nm", 1e-9 },
            { "μm", 1e-6 },
            { "mm", 1e-3 },
            { "cm", 1e-2 },
            { "dm", 1e-1 },
            { "m", 1.0 },
            { "km", 1e3},

            { "Hz", 1.0 },
            { "kHz", 1e3 },
            { "MHz", 1e6 },
            { "GHz", 1e9 },

            { "ns", 1e-9 },
            { "μs", 1e-6 },
            { "ms", 1e-3 },
            { "s", 1.0}
        };
    }
}
