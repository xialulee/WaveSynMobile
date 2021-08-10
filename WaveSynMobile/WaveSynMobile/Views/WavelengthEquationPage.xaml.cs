using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WaveSynMobile.ViewModels;

namespace WaveSynMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WavelengthEquationPage : ContentPage
    {
        public WavelengthEquationPage()
        {
            InitializeComponent();
        }

        public void OnWavelengthInputFinished(object sender, EventArgs e) {
            ((WavelengthEquationViewModel)BindingContext).Solve("wavelength");
        }
    }
}