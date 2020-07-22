using ProgMob.Models;
using ProgMob.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void Button_Clicked(object sender, EventArgs e)
        {
            Card card = new Card();
            card.Path = Name.Text;
            card.Ref = UserId;
            if (DatabaseCards.InsertCard(card))
                _ = App.Current.MainPage.DisplayAlert("Entry successful", "Please, press OK", "OK");
            else
                _ = App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
        }
    }
}