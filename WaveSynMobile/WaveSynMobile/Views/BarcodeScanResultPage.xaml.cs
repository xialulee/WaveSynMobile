﻿using System;
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
    public partial class BarcodeScanResultPage : ContentPage
    {   
        public BarcodeScanResultPage()
        {
            InitializeComponent();
        }

        public BarcodeScanResultPage(WaveSynBarcode barcode)
        {
            InitializeComponent();

            var key = System.Convert.FromBase64String(barcode.AES.Key);
            var iv = System.Convert.FromBase64String(barcode.AES.IV);
            var viewModel = new ViewModels.BarcodeScanResultViewModel(barcode.Ip, barcode.Port, barcode.Password, key, iv);
            this.BindingContext = viewModel;
            viewModel.Communicate();            
        }
    }
}