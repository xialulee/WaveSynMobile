﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquationSelectPage : ContentPage
    {
        public EquationSelectPage()
        {
            InitializeComponent();
        }

        async private void OnWavelengthEqClicked(object sender, EventArgs e)
        {
            var wavelengthEqPage = new Views.WavelengthEquationPage();
            await this.Navigation.PushAsync(wavelengthEqPage);
        }
    }
}