using System;
using System.Collections.Generic;
using System.Text;
using WaveSynMobile.Utils;

namespace WaveSynMobile.Utils {
    class Equations {
        public static void WavelengthEquation(ref double? wavelengthNumber, in string wavelengthUnit, ref double? frequencyNumber, in string frequencyUnit, ref double? periodNumber, in string periodUnit) {
            var c = Constants.lightspeed;
            var units = PhysicalQuantities.Units;
            if (wavelengthNumber is double numberOfWavelength) {
                var wavelengthValue = numberOfWavelength * units[wavelengthUnit];
                var frequencyValue = c / wavelengthValue;
                frequencyNumber = frequencyValue / units[frequencyUnit];
                var periodValue = 1 / frequencyValue;
                periodNumber = periodValue / units[periodUnit];
                return;
            }

            if (frequencyNumber is double numberOfFrequency) {
                wavelengthNumber = c / (frequencyNumber * units[frequencyUnit]) / units[wavelengthUnit];
                frequencyNumber = null;
            } else if (periodNumber is double numberOfPeriod) {
                wavelengthNumber = c * (periodNumber * units[periodUnit]) / units[wavelengthUnit];
            } // else raise Exception
            WavelengthEquation(ref wavelengthNumber, wavelengthUnit, ref frequencyNumber, frequencyUnit, ref periodNumber, periodUnit);
        }
    }
}
