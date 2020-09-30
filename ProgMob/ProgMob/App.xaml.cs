using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using Xamarin.Forms;

namespace ProgMob
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.Properties["selectedCard"] = null;
            if (Application.Current.Properties.ContainsKey("logged"))
            {
                if (Application.Current.Properties["logged"].ToString().Equals("true"))
                {
                    if (Application.Current.Properties["Admin"].ToString().Equals("true"))
                    {
                        App.Current.MainPage = new MainPageAdmin();
                        //MainPage = new NaigationPage(new MainPageAdmin());
                    }
                    else
                    {
                        App.Current.MainPage = new MainPage();
                        //MainPage = new NavigationPage(new MainPage());
                    }
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
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
