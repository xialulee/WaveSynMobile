using System;
using System.Collections.Generic;
using System.Text;
using WaveSynMobile.Utils;

namespace WaveSynMobile.ViewModels {
    class ULABeamwidthEquationViewModel : BaseViewModel {
        public double WavelengthNumber { get; set; }
        public string WavelengthUnit { get; set; }

        public double DirectionNumber { get; set; }
        public string DirectionUnit { get; set; }

        public string ApertureSizeUnit { get; set; }
        protected double? apertureSizeNumber;
        public double ApertureSizeNumber {
            get => apertureSizeNumber ?? 0.0;
            set => SetProperty(ref apertureSizeNumber, value);
        }

        public string BeamwidthUnit { get; set; }
        protected double? beamwidthNumber;
        public double BeamwidthNumber {
            get => beamwidthNumber ?? 0.0;
            set => SetProperty(ref beamwidthNumber, value);
        }

        public void Solve(string wrt) {
            double? a, θBW;
            a = θBW = null;
            switch (wrt.ToLower()) {
                case "aperture size":
                    a = ApertureSizeNumber;
                    break;
                case "beamwidth":
                    θBW = BeamwidthNumber;
                    break;
            }
            Equations.ULABeamwidthEquation(WavelengthNumber, WavelengthUnit, DirectionNumber, DirectionUnit, ref a, ApertureSizeUnit, ref θBW, BeamwidthUnit);
            ApertureSizeNumber = a ?? 0.0;
            BeamwidthNumber = θBW ?? 0.0;
        }
    }
}
