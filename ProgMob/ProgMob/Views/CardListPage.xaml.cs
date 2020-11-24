﻿using ProgMob.Popup;
using ProgMob.ViewModel;
using ProgMob.ViewModel.Helpers;
using Rg.Plugins.Popup.Services;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardListPage : ContentPage
    {
        static public int verify = 0;
        private readonly string UserId;
        CardVM CardVM;
        public CardListPage()
        {
            InitializeComponent();
            Title = "Cards";
            UserId = Application.Current.Properties["UID"].ToString();
            CardVM = Resources["CardViewModel"] as CardVM;

            if (Application.Current.Properties["Admin"].ToString().Equals("true"))
            {
                Btn_AddCard.IsVisible = true;
            }
            else
            {
                Btn_AddCard.IsVisible = false;
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
            CardVM.ListCard(UserId);
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            verify = 0;
            await PopupNavigation.PushAsync(new PopupCard(UserId));

            for (int i = 0; i < 80; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (verify != 0)
                    break;
            }

            CardVM.ListCard(UserId);
        }
    }
}