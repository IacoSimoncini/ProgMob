using ProgMob.Popup;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardListDetailPage : ContentPage
    {
        private readonly string UserId;
        private readonly string CardId;

        CardDetailVM CardDetailVM;
        public CardListDetailPage(string Uid, string Cid)
        {
            InitializeComponent();
            UserId = Uid;
            CardId = Cid;
            DatabaseDetailCard.ListExercise(UserId, CardId);
            Title = CardId;
            CardDetailVM = Resources["CardDetailViewModel"] as CardDetailVM;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CardDetailVM.ListEx(UserId, CardId);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupDetailCardl(UserId, CardId));
        }
    }
}