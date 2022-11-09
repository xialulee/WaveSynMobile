using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WaveSynMobile.ViewModels;

namespace WaveSynMobile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwowayAttenuationCoefficientsPage : ContentPage {
        public TwowayAttenuationCoefficientsPage() {
            InitializeComponent();
        }

        private async void OnFrequencyInputFinished(object sender, EventArgs e) {
            await Solve();
        }

        private async Task Solve() {
            var context = (TwowayAttenuationCoefficientsViewModel)BindingContext;
            context.Solve();
            if (context.Coefficient == double.NaN) {
                await DisplayAlert("Alert", $"The inputed frequency is invalid.", "OK");
            }
        }
    }
}