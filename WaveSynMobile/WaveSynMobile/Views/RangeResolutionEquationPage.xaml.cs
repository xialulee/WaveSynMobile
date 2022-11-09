using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WaveSynMobile.Widgets;
using WaveSynMobile.ViewModels;

namespace WaveSynMobile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RangeResolutionEquationPage : ContentPage {
        public RangeResolutionEquationPage() {
            InitializeComponent();
        }

        private async void OnRangeResolutionInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt: "range resolution");
        }

        private async void OnBandwidthInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt: "bandwidth");
        }

        private async void OnChipwidthInputFinished(object sender, EventArgs e) {
            await Solve((QuantityEntry)sender, wrt: "chipwidth");
        }

        private async Task Solve(QuantityEntry widget, string wrt) { 
            if (widget.QuantityValid && widget.QuantityNumber>0.0) {
                ((RangeResolutionEquationViewModel)BindingContext).Solve(wrt);
            } else {
                await DisplayAlert("Alert", $"The inputed {wrt} is invalid.", "OK");
            }
        }
    }
}