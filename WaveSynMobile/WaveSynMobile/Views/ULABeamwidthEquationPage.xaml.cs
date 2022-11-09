using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WaveSynMobile.Widgets;
using WaveSynMobile.ViewModels;

namespace WaveSynMobile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ULABeamwidthEquationPage : ContentPage {
        public ULABeamwidthEquationPage() {
            InitializeComponent();
        }

        private async void OnWavelengthCalculatorClicked(object sender, EventArgs e) {
            var wavelengthEqPage = new WavelengthEquationPage();
            await Navigation.PushAsync(wavelengthEqPage);
        }

        private async void OnApertureSizeInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt: "aperture size");
        }


        private async void OnBeamwidthInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt: "beamwidth");
        }

        private async Task Solve(QuantityEntry widget, string wrt) {
            if (widget.QuantityValid && widget.QuantityNumber > 0.0) {
                ((ULABeamwidthEquationViewModel)BindingContext).Solve(wrt);
            } else {
                await DisplayAlert("Alert", $"The inputed {wrt} is invalid.", "OK");
            }
        }
    }
}