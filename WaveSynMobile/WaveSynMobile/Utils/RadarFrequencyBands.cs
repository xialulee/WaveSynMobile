using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WaveSynMobile.Utils {
    class RadarFrequencyBands {
        public RadarFrequencyBands() {
            // See stackoverflow.com/questions/39367349/code-or-command-to-use-embedded-resource-in-visual-studio
            var assembly = Assembly.GetExecutingAssembly();
            var allResourceNames = assembly.GetManifestResourceNames();
            Console.WriteLine(allResourceNames[0]);
        }
    }
}
