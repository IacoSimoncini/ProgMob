using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupUpdateEx : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly string exID;
        public PopupUpdateEx(string id)
        {
            InitializeComponent();
            exID = id;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Name.Text != null && Description.Text != null && Difficulty.Text != null)
            { 
                string name = Name.Text;
                string description = Description.Text;
                string difficulty = Difficulty.Text;
                Exercise ex = new Exercise(name, description, difficulty);
                ex.Id = exID;
                bool update = await DatabaseExercise.UpdateExercise(ex);
                if (update)
                {
                    await App.Current.MainPage.DisplayAlert("Update completed", "Please, press OK to continue", "OK");
                    PopupNavigation.PopAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
                    PopupNavigation.PopAsync();
                }
            } 
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Some fields are empty", "OK");
            }
        }
    }
}