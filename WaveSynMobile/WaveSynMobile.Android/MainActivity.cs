using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using WaveSynMobile.Services;
using WaveSynMobile.Droid.Services;

namespace WaveSynMobile.Droid
{
    [Activity(Label = "WaveSynMobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = @"*/*")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            var app = new App();
            LoadApplication(app);

            DependencyService.Register<IStorage, AndroidStorage>();

            if (Intent.Action == Intent.ActionSend)
            {
                var uri = Intent.GetParcelableExtra(Intent.ExtraStream) as Android.Net.Uri;
                var stream = ContentResolver.OpenInputStream(uri);
                var navPage = app.MainPage as Xamarin.Forms.NavigationPage;
                var homePage = navPage.RootPage as WaveSynMobile.HomePage;
                var label = homePage.FindByName("FileNameLabel") as Xamarin.Forms.Label;
                label.Text = "File";
                Console.WriteLine($"The length of the stream is {stream.Length}");
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            if (requestCode == 1000) {
                if (data != null) {
                    var uri = Android.Net.Uri.Parse(data.DataString);
                }
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}