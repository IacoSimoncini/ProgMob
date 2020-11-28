using ProgMob.ViewModel.Helpers;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Splash : ContentPage
    {
        public Splash()
        {
            InitializeComponent();
            
            Startup();
        }

        public async Task Startup()
        {
            Application.Current.Properties["MyUID"] = DatabaseProfile.GetUid();
            await Application.Current.SavePropertiesAsync();
            if (await DatabaseProfile.GetProfile())
            {
                if (await DatabaseUser.ListUser() && await DatabaseExercise.ListExercise())
                {
                    App.Current.MainPage = new MainPageAdmin();
                    Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong1", "OK");
                    Navigation.PopAsync();
                }
            }
            else
            {
                if (await DatabaseCards.ListCard(DatabaseProfile.GetUid()))
                {
                    App.Current.MainPage = new MainPage();
                    Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong2", "OK");
                    Navigation.PopAsync();
                }
            }
        }
    }
}