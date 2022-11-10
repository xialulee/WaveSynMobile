using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using WaveSynMobile.Services;
using WaveSynMobile.Droid.Services;
using System.Collections.Generic;

namespace WaveSynMobile.Droid {
    using OnActivityFinished = Action<Result, Intent>;

    public static class Misc {
        public static bool TryGetAndRemove<TKey, TVal>(this IDictionary<TKey, TVal> dict, TKey key, out TVal value) {
            bool retval = dict.TryGetValue(key, out value);
            dict.Remove(key);
            return retval;
        }
    }

    [Activity(Label = "WaveSynMobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    [IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = @"*/*")]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected IDictionary<int, OnActivityFinished> requestMap = new Dictionary<int, OnActivityFinished>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            var app = new App();
            LoadApplication(app);

            DependencyService.Register<IStorage, AndroidStorage>();

            if (Intent.Action == Intent.ActionSend)
            {
                var uri = Intent.GetParcelableExtra(Intent.ExtraStream) as Android.Net.Uri;
                var stream = ContentResolver.OpenInputStream(uri);
                var navPage = app.MainPage as NavigationPage;
                var homePage = navPage.RootPage as HomePage;
                var label = homePage.FindByName("FileNameLabel") as Label;
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

        public void ActivityDo(Intent intent, OnActivityFinished action) {
            int id = 0;
            for (int i = 1000; i < 4096; ++i) {
                if (!requestMap.ContainsKey(i)) {
                    id = i;
                    break;
                }
            }
            // Todo: id==0 means no valid id found. Should raise an exception.
            requestMap[id] = action;
            StartActivityForResult(intent, id);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestMap.TryGetAndRemove(requestCode, out OnActivityFinished onFinished)) {
                onFinished(resultCode, data);
            }
        }
    }
}