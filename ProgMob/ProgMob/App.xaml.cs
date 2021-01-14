using Xamarin.Forms;

namespace ProgMob
{
    public partial class App : Application
    {
        public App()
        {
          InitializeComponent();
          if (Application.Current.Properties.ContainsKey("logged")){
                if (Application.Current.Properties["logged"].ToString().Equals("true"))
                {
                    MainPage = new NavigationPage(new Splash());
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
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
