using ProgMob.Models;
using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace ProgMob.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupUpdateCard : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly string id;
        private readonly string day;
        private readonly string path;
        private readonly string week;
        public PopupUpdateCard(string id, string day, string path, string week)
        {
            this.id = id;
            this.day = day;
            this.path = path;
            this.week = week;
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Name.Text != null)
            {
                string name = Name.Text;
                Card c = new Card();
                c.Path = name;
                c.Ref = id;
                c.Path = path;
                //(Card c, string day, string week, string newName)
                bool update = await DatabaseCards.UpdateCard(c, day, week, name);
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
                await App.Current.MainPage.DisplayAlert("Error", "Insert new name", "OK");
            }
        }
    }
}