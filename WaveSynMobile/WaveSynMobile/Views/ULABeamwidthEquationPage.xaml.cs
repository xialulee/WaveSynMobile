using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}