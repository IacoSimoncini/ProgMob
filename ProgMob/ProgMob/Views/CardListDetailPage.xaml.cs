using ProgMob.Popup;
using ProgMob.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await PopupNavigation.PushAsync(new PopupDetailCardl());
        }
    }
}