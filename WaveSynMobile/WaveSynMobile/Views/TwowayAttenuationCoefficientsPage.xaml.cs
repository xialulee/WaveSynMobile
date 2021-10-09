using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        async private void OnFrequencyInputFinished(object sender, EventArgs e) {
            await Solve();
        }

        async private Task Solve() {
            var context = (TwowayAttenuationCoefficientsViewModel)BindingContext;
            context.Solve();
            if (context.Coefficient == double.NaN) {
                await DisplayAlert("Alert", $"The inputed frequency is invalid.", "OK");
            }
        }
    }
}