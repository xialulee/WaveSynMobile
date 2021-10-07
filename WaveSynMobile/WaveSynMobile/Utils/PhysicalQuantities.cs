using System;
using System.Collections.Generic;
using System.Linq;

namespace WaveSynMobile.Utils {
    class PhysicalQuantities {
        public static readonly Dictionary<string, double> UnitsOfLength = new Dictionary<string, double> {
            { "nm", 1e-9 },
            { "μm", 1e-6 },
            { "mm", 1e-3 },
            { "cm", 1e-2 },
            { "dm", 1e-1 },
            { "m", 1.0 },
            { "km", 1e3},
        };

        public static readonly Dictionary<string, double> UnitsOfFrequency = new Dictionary<string, double> {
            { "Hz", 1.0 },
            { "kHz", 1e3 },
            { "MHz", 1e6 },
            { "GHz", 1e9 },
        };

        public static readonly Dictionary<string, double> UnitsOfTime = new Dictionary<string, double> { 
            { "ns", 1e-9 },
            { "μs", 1e-6 },
            { "ms", 1e-3 },
            { "s", 1.0}
        };

        public static readonly Dictionary<string, double> UnitsOfAngle = new Dictionary<string, double> {
            { "°", Math.PI/180.0 },
            { "rad", 1.0 },
            { "grad", Math.PI/200.0 }
        };

        public static readonly Dictionary<string, double> Units;

        static PhysicalQuantities() {
            // See https://stackoverflow.com/a/20150588.
            Units = UnitsOfLength
                .Concat(UnitsOfFrequency)
                .Concat(UnitsOfTime)
                .Concat(UnitsOfAngle)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
