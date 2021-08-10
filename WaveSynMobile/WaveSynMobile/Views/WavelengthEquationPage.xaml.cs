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
    public partial class WavelengthEquationPage : ContentPage {
        public WavelengthEquationPage() {
            InitializeComponent();
        }

        async private void OnWavelengthInputFinished(object sender, EventArgs e) {
            await Solve("wavelength", (QuantityEntry)sender);
        }

        async private void OnFrequencyInputFinished(object sender, EventArgs e) {
            await Solve("frequency", (QuantityEntry)sender);
        }
        
        async private void OnPeriodInputFinished(object sender, EventArgs e) {
            await Solve("period", (QuantityEntry)sender);
        }

        async private Task Solve(string varName, QuantityEntry widget) {
            if (widget.QuantityValid && widget.QuantityNumber>0.0) {
                ((WavelengthEquationViewModel)BindingContext).Solve(varName);
            } else {
                await DisplayAlert("Alert", $"The inputed {varName} is invalid.", "OK");
            }
        }
    }
}