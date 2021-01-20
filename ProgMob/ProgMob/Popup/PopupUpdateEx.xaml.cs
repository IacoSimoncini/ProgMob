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
        private readonly string e_name;
        private readonly string e_desc;
        private readonly string e_diff;
        public PopupUpdateEx(string id, string c_name , string c_desc , string c_diff)
        {
            InitializeComponent();
            e_name = c_name;
            e_desc = c_desc;
            e_diff = c_diff;
            exID = id;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string name = "";
            string diff = "";
            string desc = "";
            
            if (Name.Text != null){ name = Name.Text; }
            else { name = e_name; };
            if (Description.Text != null) { desc = Description.Text; }
            else { desc = e_desc; };
            if (Difficulty.SelectedItem.ToString() != null)
            {
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
            }
            else
            {
                diff = e_diff;
            }
            Console.WriteLine("AAAAAAAAAAAAAAAAA " + name + " " + desc + " " + diff);
            Exercise ex = new Exercise(name , desc , diff);
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