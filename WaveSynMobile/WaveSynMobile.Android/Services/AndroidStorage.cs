using Android.App;
using Android.Content;

using Xamarin.Essentials;

using WaveSynMobile.Services;


namespace WaveSynMobile.Droid.Services {
    class AndroidStorage : IStorage {
        private Activity activity;

        public AndroidStorage() {
            activity = Platform.CurrentActivity;
        }

        public void SaveAs(string fileName) {
            Intent intent = new Intent(Intent.ActionCreateDocument);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("*/*");
            intent.PutExtra(Intent.ExtraTitle, fileName);
            activity.StartActivityForResult(intent, 1000);
        }
    }
}