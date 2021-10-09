using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Linq;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace WaveSynMobile.Utils {
    class TwowayAttenuationInfo {
        [Name("freq/GHz")]
        public double FreqInGHz { get; set; }

        [Name("attenuation/km**-1")]
        public double Coefficient { get; set; }
    }

    class TwowayAttenuationCoefficients {
        private readonly List<TwowayAttenuationInfo> _coeffList;

        public TwowayAttenuationCoefficients() {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("WaveSynMobile.Resources.twowayattenuationcoefficients.csv"))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                var records = csv.GetRecords<TwowayAttenuationInfo>();
                _coeffList = records.ToList();
            }
        }

        public double LinearInterp(double frequencyNumber, string frequencyUnit) {
            var units = PhysicalQuantities.UnitsOfFrequency;
            double freqInGHz = frequencyNumber * units[frequencyUnit] / units["GHz"];
            if (freqInGHz < 0.1 || freqInGHz > 400) {
                return double.NaN;
            }

            for (int idx = 0; idx < _coeffList.Count() - 1; ++idx) {
                var record0 = _coeffList[idx];
                var record1 = _coeffList[idx + 1];
                var x0 = record0.FreqInGHz;
                var x1 = record1.FreqInGHz;
                var y0 = record0.Coefficient;
                var y1 = record1.Coefficient;
                if (x0 <= freqInGHz && freqInGHz <= x1) {
                    return (y1 - y0) / (x1 - x0) * (freqInGHz - x0) + y0;
                }
            }

            return double.NaN;
        }
    }
}
