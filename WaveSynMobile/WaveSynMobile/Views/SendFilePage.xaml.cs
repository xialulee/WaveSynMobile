using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSynMobile.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace WaveSynMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendFilePage : ContentPage
    {
        public SendFilePage(WaveSynBarcode barcode)
        {
            InitializeComponent();

            var key = System.Convert.FromBase64String(barcode.AES.Key);
            var iv = System.Convert.FromBase64String(barcode.AES.IV);
            var viewModel = new ViewModels.SendFileViewModel(barcode.Ip, barcode.Port, barcode.Password, key, iv);
            this.BindingContext = viewModel;

            viewModel.Communicate();
        }
    }
}