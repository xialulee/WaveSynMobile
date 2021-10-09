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
            if (freqInGHz > 400) {
                return double.NaN;
            }
            return 0.0; // To do
        }
    }
}
