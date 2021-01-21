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
        private string exID;
        private string e_name;
        private string e_desc;
        private string e_diff;
        public PopupUpdateEx(string id, string c_name, string c_desc, string c_diff)
        {
            e_name = c_name;
            e_desc = c_desc;
            e_diff = c_diff;
            exID = id;
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("1 " + e_name + " " + e_diff + " " + e_desc + " " + exID);
            if (Name.Text != null) { e_name = Name.Text; }
            if (Description.Text != null) { e_desc = Description.Text; }
            if (Difficulty.SelectedItem != null)
            {
                if (Difficulty.SelectedItem.ToString() == "Warming Up")
                {
                    e_diff = "Warming_Up.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Arms")
                {
                    e_diff = "Arms.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Sit Ups")
                {
                    e_diff = "Sit_Ups.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Back")
                {
                    e_diff = "Back.png";
                }
                else if (Difficulty.SelectedItem.ToString() == "Hit and Special")
                {
                    e_diff = "Hit_and_Special.png";
                }
            }
            Console.WriteLine("2 " + e_name + " " + e_diff + " " + e_desc + " " + exID);
            Exercise ex = new Exercise(e_name, e_desc, e_diff);
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
    }
}