using Xamarin.Forms;

namespace WaveSynMobile {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            var homePage = new HomePage();
            var navPage = new NavigationPage(homePage);
            MainPage = navPage;
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
