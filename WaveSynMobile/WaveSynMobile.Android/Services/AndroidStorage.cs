using Android.App;
using Android.Content;

using Xamarin.Essentials;

using WaveSynMobile.Services;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WaveSynMobile.Droid.Services {
    internal class AndroidStorage : IStorage {
        private readonly MainActivity activity;

        public AndroidStorage() {
            activity = (MainActivity)Platform.CurrentActivity;
        }

        public Task<Stream> AskSaveAsStream(string fileName) {
            var activityCompletion = new TaskCompletionSource<Stream>();
            Intent intent = new Intent(Intent.ActionCreateDocument);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("*/*");
            intent.PutExtra(Intent.ExtraTitle, fileName);
            activity.ActivityDo(intent, (Result resultCode, Intent data) => {
                if (data != null) {
                    var uri = Android.Net.Uri.Parse(data.DataString);
                    var stream = Application.Context.ContentResolver.OpenOutputStream(uri, "w");
                    var byteArr = Encoding.UTF8.GetBytes("Hello!");
                    stream.Write(byteArr);
                    stream.Flush();
                    activityCompletion.SetResult(stream);
                }
            });
            return activityCompletion.Task;
        }
    }
}