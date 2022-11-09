using System;
using WaveSynMobile.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendFilePage : ContentPage {
        public SendFilePage(WaveSynBarcode barcode) {
            InitializeComponent();

            byte[] key = Convert.FromBase64String(barcode.AES.Key);
            byte[] iv = Convert.FromBase64String(barcode.AES.IV);
            var viewModel = new ViewModels.SendFileViewModel(barcode.Ip, barcode.Port, barcode.Password, key, iv);
            BindingContext = viewModel;

            viewModel.Communicate();
        }
    }
}