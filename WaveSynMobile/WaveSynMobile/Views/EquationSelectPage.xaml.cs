using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquationSelectPage : ContentPage {
        public EquationSelectPage() {
            InitializeComponent();
        }

        private async void OnWavelengthEqClicked(object sender, EventArgs e) {
            var wavelengthEqPage = new WavelengthEquationPage();
            await Navigation.PushAsync(wavelengthEqPage);
        }

        private async void OnRangeResolutionEqClicked(object sender, EventArgs e) {
            var rangeResolutionEqPage = new RangeResolutionEquationPage();
            await Navigation.PushAsync(rangeResolutionEqPage);
        }

        private async void OnULABeamwidthEqClicked(object sender, EventArgs e) {
            var ulaBeamwidthEqPage = new ULABeamwidthEquationPage();
            await Navigation.PushAsync(ulaBeamwidthEqPage);
        }

        private async void OnTwowayAttenuationCoefficientsClicked(object sender, EventArgs e) {
            var twowayAttenuationCoefficientsPage = new TwowayAttenuationCoefficientsPage();
            await Navigation.PushAsync(twowayAttenuationCoefficientsPage);
        }
    }
}