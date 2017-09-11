using Xamarin.Forms;
using Estimotes;
using Beacon.Services;
using Beacon.Views;

namespace Beacon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new BeaconPage())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.Gray
            };

            EstimoteManager.Instance.Initialize().ContinueWith(r => BeaconService.OnBeaconManagerInit(r.Result));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
