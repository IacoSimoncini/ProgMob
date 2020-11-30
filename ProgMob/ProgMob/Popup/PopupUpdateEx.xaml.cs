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
            if (Name.Text != null && Description.Text != null && Difficulty.SelectedItem.ToString() != null)
            {
                string diff = "";
                if (Difficulty.SelectedItem.ToString() == "Warming Up")
                {
                    diff = "Warming_Up.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Arms")
                {
                    diff = "Arms.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Sit Ups")
                {
                    diff = "Sit_Ups.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Back")
                {
                    diff = "Back.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Hit and Special")
                {
                    diff = "Hit_and_Special.png";
                }
                string name = Name.Text;
                string description = Description.Text;
                Exercise ex = new Exercise(name, description, diff);
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