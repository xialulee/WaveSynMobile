using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace WaveSynMobile.Utils {
    class RadarFrequencyBandInfo {
        [Name("band_name")]
        public string Name { get; set; }

        [Name("min_freq/GHz")]
        public double MinFreqInGHz { get; set; }

        [Name("max_freq/GHz")]
        public double MaxFreqInGHz { get; set; }
    }

    class RadarFrequencyBands {
        private readonly List<RadarFrequencyBandInfo> _bandInfoList;

        public RadarFrequencyBands() {
            // See stackoverflow.com/questions/39367349/code-or-command-to-use-embedded-resource-in-visual-studio
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("WaveSynMobile.Resources.radarfrequencybands.csv"))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                var records = csv.GetRecords<RadarFrequencyBandInfo>();
                _bandInfoList = records.ToList();
            }
        }

        public string GetName(double frequencyNumber, string frequencyUnit) {
            var units = PhysicalQuantities.UnitsOfFrequency;
            double freqInGHz = frequencyNumber * units[frequencyUnit] / units["GHz"];
            return (from bandInfo in _bandInfoList 
                    where bandInfo.MinFreqInGHz <= freqInGHz && freqInGHz < bandInfo.MaxFreqInGHz 
                    select bandInfo.Name)
                   .FirstOrDefault();
        }
    }
}
