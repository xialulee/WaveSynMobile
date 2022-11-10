using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveSynMobile.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecvClipboardTextPage : ContentPage {
        public RecvClipboardTextPage() {
            InitializeComponent();
        }

        public RecvClipboardTextPage(WaveSynBarcode barcode) {
            InitializeComponent();
            byte[] key = Convert.FromBase64String(barcode.AES.Key);
            byte[] iv = Convert.FromBase64String(barcode.AES.IV);
            var viewModel = new ViewModels.RecvClipboardTextViewModel(barcode.Ip, barcode.Port, barcode.Password, key, iv);
            BindingContext = viewModel;
            viewModel.Communicate();
        }
    }
}