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

            if (Application.Current.Properties["Admin"].ToString().Equals("true"))
            {
                Btn_Add.IsVisible = true;
            }
            else
            {
                Btn_Add.IsVisible = false;
            }

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
                    await DisplayAlert("Logout", "You have been disconnected, the app will be closed", "Cancel");
                    await System.Threading.Tasks.Task.Delay(1000);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else await DisplayAlert("Logout", "It was not possible to disconnect", "Cancel");
            };
            this.ToolbarItems.Add(TBItem);


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