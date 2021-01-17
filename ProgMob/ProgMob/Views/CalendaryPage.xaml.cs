
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
        public CalendaryVM CalendaryVM;
        private readonly string UserId;
        private readonly string type;
        public CalendaryPage()
        {
            InitializeComponent();
            Title = "Calendary";
            type = "A"; //Application.Current.Properties["ABC"].ToString();
            UserId = Application.Current.Properties["UID"].ToString();
            CalendaryVM = Resources["CalendaryViewModel"] as CalendaryVM;

            ToolbarItem TBItem = new ToolbarItem
            {
                Text = "Change Type",
                IconImageSource = ImageSource.FromFile("logout.png"),
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            TBItem.Clicked += async (sender, args) =>
            {
                Console.WriteLine("AO");
            };

            ToolbarItem TBItemLogout = new ToolbarItem
            {
                Text = "Logout",
                IconImageSource = ImageSource.FromFile("logout.png"),
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };

            TBItemLogout.Clicked += async (sender, args) =>
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
            this.ToolbarItems.Add(TBItemLogout);
            this.ToolbarItems.Add(TBItem);
        }


        protected override void OnAppearing()
        {
            CalendaryVM.ListDaysInWeek(UserId, type);
            base.OnAppearing();
        }

        public void OnTapped(object sender, EventArgs e)
        {
           DaysInWeek d =  sender as DaysInWeek;
            Console.WriteLine(sender);
        }
    }
}