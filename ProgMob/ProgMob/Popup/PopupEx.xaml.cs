using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEx : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupEx()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Exercise ex = new Exercise(Name.Text, Description.Text, Difficulty.Text);
            if (DatabaseExercise.InsertExercise(ex))
                await App.Current.MainPage.DisplayAlert("Insert completed", "Please, press OK to continue", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
        }
    }
}