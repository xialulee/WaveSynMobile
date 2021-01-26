using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WaveSynMobile
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        async private void OnScanClicked(object sender, EventArgs e)
        {
            var scanPage = new Pages.BarcodeScanPage();
            await this.Navigation.PushAsync(scanPage);
        }
    }
}
