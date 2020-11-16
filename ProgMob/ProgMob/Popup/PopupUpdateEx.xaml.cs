using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupUpdateEx : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupUpdateEx()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string name = Name.Text;
            string description = Description.Text;
            string difficulty = Difficulty.Text;
            Exercise ex = new Exercise(name, description, difficulty);
            bool update = await DatabaseExercise.UpdateExercise(ex);
            if (update)
                await App.Current.MainPage.DisplayAlert("Update completed", "Please, press OK to continue", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
        }
    }
}