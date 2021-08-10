using System;
using System.Collections.Generic;
using System.Text;
using WaveSynMobile.Utils;

namespace WaveSynMobile.ViewModels {
    class WavelengthEquationViewModel : BaseViewModel {
        public string WavelengthUnit {
            get; set;
        }
        public string FrequencyUnit {
            get; set;
        }
        public string PeriodUnit {
            get; set;
        }

        protected double? wavelengthNumber;
        protected double? frequencyNumber;
        protected double? periodNumber;
        
        public double WavelengthNumber {
            get => wavelengthNumber ?? -1; // Maybe NaN?
            set => SetProperty(ref wavelengthNumber, value);
        }

        public double FrequencyNumber {
            get => frequencyNumber ?? -1; // Maybe NaN;
            set => SetProperty(ref frequencyNumber, value);
        }

        public double PeriodNumber {
            get => periodNumber ?? -1; // Maybe NaN;
            set => SetProperty(ref periodNumber, value);
        }

        public void Solve(string varName) {
            double? λ = null, f = null, T = null;
            switch (varName.ToLower()) {
                case "wavelength":
                    λ = WavelengthNumber;
                    break;
                case "frequency":
                    f = FrequencyNumber;
                    break;
                case "period":
                    T = PeriodNumber;
                    break;
            }
            Equations.WavelengthEquation(ref λ, WavelengthUnit, ref f, FrequencyUnit, ref T, PeriodUnit);
            WavelengthNumber = λ ?? -1;
            FrequencyNumber = f ?? -1;
            PeriodNumber = T ?? -1;
        }
    }
}
