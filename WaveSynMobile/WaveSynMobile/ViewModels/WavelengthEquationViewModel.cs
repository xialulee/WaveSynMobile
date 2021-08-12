using System;
using System.Collections.Generic;
using System.Text;
using WaveSynMobile.Utils;

namespace WaveSynMobile.ViewModels {
    class WavelengthEquationViewModel : BaseViewModel {
        private RadarFrequencyBands _radarBands;

        public WavelengthEquationViewModel() {
            _radarBands = new RadarFrequencyBands();
        }

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
        protected string frequencyBandName;
        
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

        public string FrequencyBandName {
            get => frequencyBandName;
            set => SetProperty(ref frequencyBandName, value);
        }

        public void Solve(string wrt) {
            double? λ, f, T;
            λ = f = T = null;
            switch (wrt.ToLower()) {
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
            FrequencyBandName = _radarBands.GetName(FrequencyNumber, FrequencyUnit);
        }
    }
}
