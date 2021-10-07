using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        async private void OnWavelengthCalculatorClicked(object sender, EventArgs e) {
            var wavelengthEqPage = new WavelengthEquationPage();
            await Navigation.PushAsync(wavelengthEqPage);
        }

        async private void OnApertureSizeInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt: "aperture size");
        }


        async private void OnBeamwidthInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt: "beamwidth");
        }

        async private Task Solve(QuantityEntry widget, string wrt) {
            if (widget.QuantityValid && widget.QuantityNumber > 0.0) {
                ((ULABeamwidthEquationViewModel)BindingContext).Solve(wrt);
            } else {
                await DisplayAlert("Alert", $"The inputed {wrt} is invalid.", "OK");
            }
        }
    }
}