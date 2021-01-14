using ProgMob.Models;
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
    public partial class CalendaryPage : ContentPage
    {
        static public int verify = 0;
        CalendaryVM CalendaryVM;
        private readonly string UserId;
        public CalendaryPage()
        {
            InitializeComponent();
            Title = "Calendary";
            UserId = Application.Current.Properties["UID"].ToString();
            CalendaryVM = Resources["CalendaryViewModel"] as CalendaryVM;

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
            CalendaryVM.ListDay(UserId, "A");
        }

        private async void ButtonAdd_Clicked(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            var day = btn.BindingContext as Day;
            
            verify = 0;
            await PopupNavigation.PushAsync(new PopupCard(UserId, day.n.ToString()));

            for (int i = 0; i < 1000; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (verify != 0)
                    break;
            }

            CalendaryVM.ListDay(UserId, "A");
            
        }
       
        private async void ButtonGo_Clicked(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            var day = btn.BindingContext as Day;
            Application.Current.Properties["selectedDay"] = day.n.ToString();
            Application.Current.SavePropertiesAsync();
            App.Current.MainPage.Navigation.PushAsync(new CardListPage());

        }

        /*
        private void CalendaryView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //if (sender is ListView lv) lv.SelectedItem = null;
        }*/

    }
}