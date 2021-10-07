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
            } else if (periodNumber is double numberOfPeriod) {
                wavelengthNumber = c * (periodNumber * units[periodUnit]) / units[wavelengthUnit];
            } // else raise Exception
            WavelengthEquation(ref wavelengthNumber, wavelengthUnit, ref frequencyNumber, frequencyUnit, ref periodNumber, periodUnit);
        }

        public static void RangeResolutionEquation(
                ref double? rangeResolutionNumber, string rangeResolutionUnit, 
                ref double? bandwidthNumber, string bandwidthUnit,
                ref double? chipwidthNumber, string chipwidthUnit) {
            var c = Constants.lightspeed;
            var units = PhysicalQuantities.Units;

            if (rangeResolutionNumber is double ΔRnum) {
                var ΔR = ΔRnum * units[rangeResolutionUnit];
                var Bw = c / 2 / ΔR;
                bandwidthNumber = Bw / units[bandwidthUnit];
                chipwidthNumber = 1 / Bw / units[chipwidthUnit];
                return;
            }

            if (bandwidthNumber is double Bwnum) {
                rangeResolutionNumber = c / 2 / (Bwnum * units[bandwidthUnit]) / units[rangeResolutionUnit];
            } else if (chipwidthNumber is double Cwnum) {
                rangeResolutionNumber = c / 2 * (Cwnum * units[chipwidthUnit]) / units[rangeResolutionUnit];
            }
            RangeResolutionEquation(ref rangeResolutionNumber, rangeResolutionUnit, ref bandwidthNumber, bandwidthUnit, ref chipwidthNumber, chipwidthUnit);
        }

        public static void ULABeamwidthEquation(
                in double wavelengthNumber, in string wavelengthUnit, 
                in double directionNumber, in string directionUnit,
                ref double? apertureSizeNumber, in string apertureSizeUnit,
                ref double? beamwidthNumber, in string beamwidthUnit) {
            var k = 0.886; // 3dB width. Maybe add an option for 4dB width (k=1)
            var units = PhysicalQuantities.Units;
            var λ = wavelengthNumber * units[wavelengthUnit];
            var θ0 = directionNumber * units[directionUnit];

            if (apertureSizeNumber is double aNum) {
                var a = aNum * units[apertureSizeUnit];
                var θBW = k * λ / (a * Math.Cos(θ0));
                beamwidthNumber = θBW / units[beamwidthUnit];
                return;
            }

            if (beamwidthNumber is double θBWNum) {
                var θBW = θBWNum * units[beamwidthUnit];
                var a = k * λ / (θBW*Math.Cos(θ0));
                apertureSizeNumber = a / units[apertureSizeUnit];
                return;
            }
        }
    }
}
