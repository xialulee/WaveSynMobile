using System;
using System.Collections.Generic;
using System.Text;
using WaveSynMobile.Utils;

namespace WaveSynMobile.ViewModels {
    class TwowayAttenuationCoefficientsViewModel : BaseViewModel {
        private TwowayAttenuationCoefficients _coefficients;

        public TwowayAttenuationCoefficientsViewModel() {
            _coefficients = new TwowayAttenuationCoefficients();
        }

        protected double coefficient;

        public double Coefficient {
            get => coefficient;
            set => SetProperty(ref coefficient, value);
        }

        public string FrequencyUnit {
            get; set;
        }

        protected double frequencyNumber;

        public double FrequencyNumber {
            get => frequencyNumber;
            set => SetProperty(ref frequencyNumber, value);
        }

        public void Solve() {
            Coefficient = _coefficients.LinearInterp(FrequencyNumber, FrequencyUnit);
        }
    }
}
