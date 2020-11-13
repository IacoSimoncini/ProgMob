using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Application.Current.Properties["Admin"] = DatabaseProfile.GetProfile();
            Startup();
        }

        public async Task Startup()
        {
            Console.WriteLine(Application.Current.Properties["Admin"].ToString());
            if (Application.Current.Properties["Admin"].ToString().Equals("true"))
            {
                if(await DatabaseUser.ListUser())
                {
                    App.Current.MainPage = new MainPageAdmin();
                    Navigation.RemovePage(this);
                }
                else
                {
                    await DisplayAlert("Error", "Something went wrong1", "OK");
                    Navigation.PopAsync();
                }
            }
            else
            { 
                if(await DatabaseCards.ListCard(DatabaseProfile.GetUid()))
                {
                    App.Current.MainPage = new MainPage();
                    Navigation.RemovePage(this);
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