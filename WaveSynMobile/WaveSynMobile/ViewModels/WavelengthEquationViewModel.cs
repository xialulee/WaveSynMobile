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
            get => wavelengthNumber ?? 0.0; 
            set => SetProperty(ref wavelengthNumber, value);
        }

        public double FrequencyNumber {
            get => frequencyNumber ?? 0.0; 
            set => SetProperty(ref frequencyNumber, value);
        }

        public double PeriodNumber {
            get => periodNumber ?? 0.0; 
            set => SetProperty(ref periodNumber, value);
        }

        public void Solve(string varName) {
            double? λ, f, T;
            λ = f = T = null;
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
            WavelengthNumber = λ ?? 0.0;
            FrequencyNumber = f ?? 0.0;
            PeriodNumber = T ?? 0.0;
        }
    }
}
