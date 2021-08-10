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
            await Solve((QuantityEntry)sender, wrt:"wavelength");
        }

        async private void OnFrequencyInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt:"frequency");
        }
        
        async private void OnPeriodInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt:"period");
        }

        async private Task Solve(QuantityEntry widget, string wrt) {
            if (widget.QuantityValid && widget.QuantityNumber>0.0) {
                ((WavelengthEquationViewModel)BindingContext).Solve(wrt);
            } else {
                await DisplayAlert("Alert", $"The inputed {wrt} is invalid.", "OK");
            }
        }
    }
}