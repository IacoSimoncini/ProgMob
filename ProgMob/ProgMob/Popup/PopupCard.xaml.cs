using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using ProgMob.Views;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupCard : Rg.Plugins.Popup.Pages.PopupPage
    {
        private string UserId;
        public PopupCard(string Uid)
        {
            InitializeComponent();
            UserId = Uid;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Card card = new Card();
            card.Path = Name.Text;
            card.Ref = UserId;
            if (DatabaseCards.InsertCard(card)) {
                if (await DatabaseCards.ListCard(UserId))
                { 
                    _ = App.Current.MainPage.DisplayAlert("Entry successful", "Please, press OK", "OK");
                    CardListPage.verify = 1;
                }
                else
                {
                    _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
                }
            } 
            else
            {
                _ = App.Current.MainPage.DisplayAlert("Error", "The insertion was not successful", "OK");
            }
            PopupNavigation.PopAsync();
        }
    }
}