﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeScanPage : ContentPage
    {
        private bool Scanning = true;

        public BarcodeScanPage()
        {
            InitializeComponent();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            if (Scanning)
            {
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Utils.WaveSynBarcode barcode;
                    try
                    {
                        barcode = JsonSerializer.Deserialize<Utils.WaveSynBarcode>(result.Text);
                    }
                    catch (JsonException ex) 
                    {
                        return;
                    }
                    Scanning = false;

                    switch (barcode.Command.Action, barcode.Command.Source)
                    {
                        case ("read", "clipboard"):
                            var sendClipboardTextPage = new Views.SendClipboardTexttPage(barcode);
                            await this.Navigation.PushAsync(sendClipboardTextPage);
                            this.Navigation.RemovePage(this);
                            break;
                        case ("read", "storage"):
                            var sendFilePage = new Views.SendFilePage(barcode);
                            await this.Navigation.PushAsync(sendFilePage);
                            this.Navigation.RemovePage(this);
                            break;
                    }
                });
            }
        }
    }
}