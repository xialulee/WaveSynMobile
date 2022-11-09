using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WaveSynMobile.Utils;

namespace WaveSynMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendClipboardTextPage : ContentPage {
        public SendClipboardTextPage() {
            InitializeComponent();
        }

        public SendClipboardTextPage(WaveSynBarcode barcode) {
            InitializeComponent();

            byte[] key = Convert.FromBase64String(barcode.AES.Key);
            byte[] iv = Convert.FromBase64String(barcode.AES.IV);
            var viewModel = new ViewModels.SendClipboardTextViewModel(
                barcode.Ip, barcode.Port, barcode.Password, key, iv);
            BindingContext = viewModel;
            viewModel.Communicate();
        }
    }
}