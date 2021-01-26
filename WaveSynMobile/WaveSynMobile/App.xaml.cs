﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaveSynMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var homePage = new HomePage();
            var navPage = new NavigationPage(homePage);
            this.MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
