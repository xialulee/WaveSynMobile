using System;
using System.Collections.Generic;
using System.Text;
using WaveSynMobile.Utils;

namespace WaveSynMobile.ViewModels {
    class RangeResolutionEquationViewModel : BaseViewModel {
        public string RangeResolutionUnit {
            get; set;
        }

        public string BandwidthUnit {
            get; set;
        }

        public string ChipwidthUnit {
            get; set;
        }

        protected double? rangeResolutionNumber;
        protected double? bandwidthNumber;
        protected double? chipwidthNumber;

        public double RangeResolutionNumber {
            get => rangeResolutionNumber ?? 0.0;
            set => SetProperty(ref rangeResolutionNumber, value);
        }

        public double BandwidthNumber {
            get => bandwidthNumber ?? 0.0;
            set => SetProperty(ref bandwidthNumber, value);
        }

        public double ChipwidthNumber {
            get => chipwidthNumber ?? 0.0;
            set => SetProperty(ref chipwidthNumber, value);
        }

        public void Solve(string wrt) {
            double? ΔR, Bw, Cw;
            ΔR = Bw = Cw = null;
            switch (wrt.ToLower()) {
                case "range resolution":
                    ΔR = RangeResolutionNumber;
                    break;
                case "bandwidth":
                    Bw = BandwidthNumber;
                    break;
                case "chipwidth":
                    Cw = ChipwidthNumber;
                    break;
            }
            Equations.RangeResolutionEquation(ref ΔR, RangeResolutionUnit, ref Bw, BandwidthUnit, ref Cw, ChipwidthUnit);
            RangeResolutionNumber = ΔR ?? 0.0;
            BandwidthNumber = Bw ?? 0.0;
            ChipwidthNumber = Cw ?? 0.0;
        }
    }
}
