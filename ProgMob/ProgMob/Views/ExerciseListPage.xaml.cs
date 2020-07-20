using ProgMob.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ProgMob.Popup;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseListPage : ContentPage
    {
        ExerciseVM ExVM;
        public ExerciseListPage()
        {
            InitializeComponent();
            Title = "Exercises";

            ExVM = Resources["ExVM"] as ExerciseVM;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ExVM.ListExercise();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupEx());
        }
    }
}