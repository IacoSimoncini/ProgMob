using ProgMob.Popup;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
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
            Console.WriteLine("PROVA : 1)" + UserId, " 2)" + CardId);
            Title = CardId;
            CardDetailVM = Resources["CardDetailViewModel"] as CardDetailVM;

            ToolbarItem TBItem = new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = ImageSource.FromFile("logout.png"),
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            TBItem.Clicked += async (sender, args) =>
            {
                bool logout = await Auth.Logout();
                if (logout)
                {
                    Application.Current.Properties["logged"] = "false";
                    await Application.Current.SavePropertiesAsync();
                    await DisplayAlert("Logout", "You have been disconnected", "Cancel");
                    await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage(), true);
                }
                else await DisplayAlert("Logout", "It was not possible to disconnect", "Cancel");
            };
            this.ToolbarItems.Add(TBItem);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CardDetailVM.ListEx(UserId, CardId);
            Console.WriteLine("ListDP : 1)" + UserId, " 2)" + CardId);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopupDetailCardl(UserId , CardId));
        }
    }
}